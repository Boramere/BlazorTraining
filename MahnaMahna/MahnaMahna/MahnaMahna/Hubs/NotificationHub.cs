using MahnaMahna.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace MahnaMahna.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification(TodoItem item)
    {
        await Clients.All.SendAsync("TodoChanged", item);
    }
}