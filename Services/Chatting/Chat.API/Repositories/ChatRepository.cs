using Chat.API.Models;
using Marten;

namespace Chat.API.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IDocumentSession _session;

        public ChatRepository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Conversation> GetOrCreateConversationAsync(Guid userId, string userName, CancellationToken cancellationToken)
        {
            var conversation = await _session.Query<Conversation>()
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    UserName = userName,
                    LastMessage = string.Empty,
                    LastMessageTime = DateTime.UtcNow,
                    Messages = new List<ChatMessage>()
                };

                _session.Store(conversation);
            }

            await _session.SaveChangesAsync(cancellationToken);
            return conversation;
        }

        public async Task<IEnumerable<Conversation>> GetAdminConversationsAsync(CancellationToken cancellationToken)
        {
            return await _session.Query<Conversation>()
                .OrderByDescending(c => c.LastMessageTime)
				.ToListAsync(cancellationToken);
        }

        public async Task<ChatMessage> AddMessageAsync(Guid conversationId, string content, bool isAdmin, CancellationToken cancellationToken)
        {
            var conversation = await _session.LoadAsync<Conversation>(conversationId, cancellationToken);
            if (conversation == null)
                throw new ArgumentException("Conversation not found");

            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                Content = content,
                Time = DateTime.UtcNow,
                Sent = isAdmin,
                ConversationId = conversationId
			};

            conversation.Messages.Add(message);
            conversation.LastMessage = content;
            conversation.LastMessageTime = message.Time;

            _session.Update(conversation);
            await _session.SaveChangesAsync(cancellationToken);
            return message;
        }
    }
} 