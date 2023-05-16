using Microsoft.AspNetCore.SignalR;

namespace WMS.API.Hubs.Notification;

public class UserNotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}