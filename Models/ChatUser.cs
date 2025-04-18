using System;
using System.ComponentModel.DataAnnotations;

namespace ChatService.Models
{
    public class ChatUser
    {
        [Key]
        public string Id { get; set; }  // This will store the user's unique identifier

        public string UserName { get; set; }  // Optionally store a username

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
