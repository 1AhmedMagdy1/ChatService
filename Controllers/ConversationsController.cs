using ChatService.Data;
using ChatService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly ChatDbContext _context;

        public ConversationsController(ChatDbContext context)
        {
            _context = context;
        }

        // GET: api/Conversations/user/{userId}
        // Returns all conversations for a given user.
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserConversations(string userId)
        {
            var conversations = await _context.Conversations
                .Where(c => c.UserAId == userId || c.UserBId == userId)
                .Include(c => c.ChatMessages)
                .ToListAsync();

            var conversationDtos = await Task.WhenAll(conversations.Select(async c =>
            {
                // Determine the partner: if the current user is UserA then partner is UserB, and vice versa.
                var partnerId = c.UserAId == userId ? c.UserBId : c.UserAId;
                var partner = await _context.ChatUsers.FindAsync(partnerId);

                // Get the last message, if it exists.
                var lastMessage = c.ChatMessages.OrderByDescending(m => m.CreatedAt).FirstOrDefault();

                return new
                {
                    conversationId = c.Id,
                    partnerId = partnerId,
                    partnerName = partner?.UserName,
                    lastMessageContent = lastMessage?.MessageContent,
                    lastMessageTime = lastMessage?.CreatedAt
                };
            }));


            return Ok(conversationDtos);
        }

        // GET: api/Conversations/{conversationId}
        // Returns the conversation details including messages.
        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetConversationById(int conversationId)
        {
            var conversation = await _context.Conversations
                .Include(c => c.ChatMessages.OrderBy(m => m.CreatedAt))
                .FirstOrDefaultAsync(c => c.Id == conversationId);
            var userAid=conversation.UserAId;
            var userBid = conversation.UserBId;
            var userAname = (await _context.ChatUsers.FirstOrDefaultAsync(c => c.Id == userAid))?.UserName;
            var userBname = (await _context.ChatUsers.FirstOrDefaultAsync(c => c.Id == userBid))?.UserName;


            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                conversationId = conversation.Id,
                userAid = userAid,
                userBid = userBid,
                userAname = userAname,
                userBname=userBname,
                chatMessages = conversation.ChatMessages.Select(m => new {
                    senderId = m.SenderId,
                    messageContent = m.MessageContent,
                    messageType = m.MessageType,
                    createdAt = m.CreatedAt
                })
            });
        }
    }
}
