using System;

namespace ChatService.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        // Foreign key to the Conversation
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public string SenderId { get; set; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; } // e.g., "text", "image", "file"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
