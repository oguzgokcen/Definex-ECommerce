namespace Chat.API.Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAdmin { get; set; }
    }
} 