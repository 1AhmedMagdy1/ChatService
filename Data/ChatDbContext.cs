using ChatService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChatService.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }  // New table for storing user IDs and info
    }
}
