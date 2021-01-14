using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ColorChat.SignalR.Hubs
{
    public class DoorManagementHub : Hub
    {
        public async Task SendMessage(string value)
        {
            await Clients.All.SendAsync("ReceiveMessage", value);
        }
    }
}
