using Chat.API.Models;

namespace Chat.API.Repositories
{
    public interface IChatRepository
    {
        Task<Conversation> GetOrCreateConversationAsync(Guid userId, string userName, CancellationToken cancellationToken);
        Task<IEnumerable<Conversation>> GetAdminConversationsAsync(CancellationToken cancellationToken);
        Task<ChatMessage> AddMessageAsync(Guid conversationId, string content, bool isAdmin, CancellationToken cancellationToken);
    }
} 