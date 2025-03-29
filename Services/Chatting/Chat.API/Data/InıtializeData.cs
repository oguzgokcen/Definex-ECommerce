using Marten.Schema;
using Marten;

namespace Chat.API.Data
{
	//public class InıtializeData : IInitialData
	//{
	//	public async Task Populate(IDocumentStore store, CancellationToken cancellation)
	//	{
	//		using var session = store.LightweightSession();

	//		if (await session.Query<ChatMessage>().AnyAsync())
	//			return;

	//		var conversation = new Conversation
	//		{
	//			Id = Guid.NewGuid(),
	//			UserId = Guid.NewGuid(),
	//			UserName = "DummyUser",
	//			LastMessage = "Hello World!",
	//			LastMessageTime = DateTime.UtcNow,
	//			Messages = new List<ChatMessage>
	//			{
	//				new ChatMessage
	//				{
	//					Id = Guid.NewGuid(),
	//					Content = "Hello World!",
	//					Time = DateTime.UtcNow,
	//					Sent = true
	//				}
	//			}
	//		};

	//		session.Store(conversation);
	//		await session.SaveChangesAsync();
	//	}
	//}
}
