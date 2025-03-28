using Microsoft.AspNetCore.SignalR;
using Chat.API.Models;
using System.Security.Claims;

namespace Chat.API.Hubs;

public class ChatHub : Hub
{
    private static readonly Dictionary<Guid, string> _userConnections = new();
    private static readonly Dictionary<Guid, bool> _adminUsers = new();

    public override async Task OnConnectedAsync()
    {
        var user =Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = Context.User?.IsInRole("Admin") ?? false;

        if (user != null)
        {
            var userId = Guid.Parse(user);
			_userConnections[userId] = Context.ConnectionId;
            _adminUsers[userId] = isAdmin;

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
        var user = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (user != null)
        {
			var userId = Guid.Parse(user);
			_userConnections.Remove(userId);
            _adminUsers.Remove(userId);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessageToUser(Guid receiverId, string message)
    {
        if (_userConnections.TryGetValue(receiverId, out string? connectionId))
        {
            var chatMessage = new ChatMessage
            {
                Message = message,
                SenderId = Guid.Empty,
                ReceiverId = receiverId,
                Timestamp = DateTime.UtcNow,
                IsAdmin = true
            };

            await Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage);
            await Clients.Caller.SendAsync("MessageSent", chatMessage);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveError", "User not found or not connected");
        }
    }

    public async Task SendMessageToAdmin(string message)
    {
        var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (senderId == null) return;

        var chatMessage = new ChatMessage
        {
            Message = message,
            SenderId = Guid.Parse(senderId),
            ReceiverId = Guid.Empty,
            Timestamp = DateTime.UtcNow,
            IsAdmin = false
        };

        await Clients.Group("Admins").SendAsync("ReceiveMessage", chatMessage);
        await Clients.Caller.SendAsync("MessageSent", chatMessage);
    }
}

