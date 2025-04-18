using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatService.Data;
using ChatService.Models;

namespace ChatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatUsersController : ControllerBase
    {
        private readonly ChatDbContext _context;

        public ChatUsersController(ChatDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatUser>>> GetChatUsers()
        {
            return await _context.ChatUsers.ToListAsync();
        }

        // GET: api/ChatUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatUser>> GetChatUser(string id)
        {
            var chatUser = await _context.ChatUsers.FindAsync(id);

            if (chatUser == null)
            {
                return NotFound();
            }

            return chatUser;
        }

        // PUT: api/ChatUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatUser(string id, ChatUser chatUser)
        {
            if (id != chatUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ChatUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatUser>> PostChatUser(ChatUser chatUser)
        {
            _context.ChatUsers.Add(chatUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChatUserExists(chatUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChatUser", new { id = chatUser.Id }, chatUser);
        }

        // DELETE: api/ChatUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatUser(string id)
        {
            var chatUser = await _context.ChatUsers.FindAsync(id);
            if (chatUser == null)
            {
                return NotFound();
            }

            _context.ChatUsers.Remove(chatUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatUserExists(string id)
        {
            return _context.ChatUsers.Any(e => e.Id == id);
        }

        // GET: api/ChatUsers/exists/{id}
        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> UserExists(string id)
        {
            bool exists = await _context.ChatUsers.AnyAsync(e => e.Id == id);
            return Ok(exists);
        }

    }
}
