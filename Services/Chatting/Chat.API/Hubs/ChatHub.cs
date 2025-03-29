using Chat.API.Models;
using Chat.API.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
	private readonly IChatRepository _chatRepository;

	public ChatHub(IChatRepository chatRepository)
	{
		_chatRepository = chatRepository;
	}

	public override async Task OnConnectedAsync()
	{
		var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var userName = Context.User?.FindFirst("name")?.Value ?? "user";
		var isAdmin = KeycloakHelper.UserHasRealmRole(Context.User, "admin");

		if (userId != null)
		{
			if (isAdmin)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
			}
			else
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, "Customers");
			}
		}

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (userId != null)
		{
			var isAdmin = KeycloakHelper.UserHasRealmRole(Context.User, "admin");
			if (isAdmin)
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
			}
			else
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Customers");
			}
		}

		await base.OnDisconnectedAsync(exception);
	}

	public async Task SendMessageToUser(Guid receiverId, string message)
	{
		var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (senderId == null) return;

		var conversation = await _chatRepository.GetOrCreateConversationAsync(
			receiverId,
			Context.User!.FindFirst("name")!.Value,
			CancellationToken.None);

		var newMessage = await _chatRepository.AddMessageAsync(
			conversation.Id,
			message,
			true,
			CancellationToken.None);

		await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", newMessage);
		await Clients.Caller.SendAsync("MessageSent", newMessage);
	}

	public async Task SendMessageToAdmin(string message)
	{
		var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var senderName = Context.User?.FindFirst("name")?.Value;
		if (senderId == null || senderName == null) return;

		var conversation = await _chatRepository.GetOrCreateConversationAsync(
			Guid.Parse(senderId),
			senderName,
			CancellationToken.None);

		var newMessage = await _chatRepository.AddMessageAsync(
			conversation.Id,
			message,
			false,
			CancellationToken.None);

		await Clients.Group("Admins").SendAsync("ReceiveMessage", newMessage);
		await Clients.Caller.SendAsync("MessageSent", newMessage);
	}
}