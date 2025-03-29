using Marten;

namespace Chat.API.Models
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public List<ChatMessage> Messages { get; set; } = new();
    }
} 