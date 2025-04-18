using System;
using System.Collections.Generic;

namespace ChatService.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string UserAId { get; set; }
        public string UserBId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to related messages
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
