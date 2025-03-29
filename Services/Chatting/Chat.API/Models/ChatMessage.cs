namespace Chat.API.Models
{
	public class ChatMessage
	{
		public Guid Id { get; set; }
		public string Content { get; set; }
		public DateTime Time { get; set; }
		public bool Sent { get; set; }
		public Guid ConversationId { get; set; }
	}
} 