using ChatService.Data;
using ChatService.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatDbContext _context;

        public ChatHub(ChatDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string senderId, string receiverId, string message, string type)
        {
            var conversation = await _context.Conversations.FirstOrDefaultAsync(c =>
                (c.UserAId == senderId && c.UserBId == receiverId) ||
                (c.UserAId == receiverId && c.UserBId == senderId));

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    UserAId = senderId,
                    UserBId = receiverId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            var chatMessage = new ChatMessage
            {
                ConversationId = conversation.Id,
                SenderId = senderId,
                MessageContent = message,
                MessageType = type,
                CreatedAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message, type, conversation.Id);
            await Clients.Caller.SendAsync("ReceiveMessage", senderId, message, type, conversation.Id);
        }
    }
}