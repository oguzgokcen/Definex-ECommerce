using Chat.API.Hubs;
using Chat.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers;
public class ChatController : Controller
{
	private readonly IHubContext<ChatHub> _chatHub;
	private readonly IChatRepository _chatRepository;
	public ChatController(IHubContext<ChatHub> chatHub, IChatRepository chatRepository)
	{
		_chatHub = chatHub;
		_chatRepository = chatRepository;
	}

	[HttpGet]
	[Route("chat/conversations")]
	[AdminAuthorize]
	public async Task<IActionResult> GetConversations()
	{
		var messages = await _chatRepository.GetAdminConversationsAsync(CancellationToken.None);
		return messages != null ? Ok(messages) : NotFound();
	}
}
