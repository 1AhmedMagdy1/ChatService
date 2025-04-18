namespace ChatService.Models
{
    public class ConversationDto
    {
        public int ConversationId { get; set; }
        public string PartnerId { get; set; }
        public string LastMessageContent { get; set; }
        public DateTime? LastMessageTime { get; set; }
    }
}
