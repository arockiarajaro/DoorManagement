using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementClient.ClientTransporter
{
   
   public class SignalRMessageService
    {
        private readonly HubConnection _connection;

        public event Action<string> MessageReceived;

        public SignalRMessageService(HubConnection connection)
        {
            _connection = connection;

            _connection.On<string>("ReceiveMessage", (v) => MessageReceived?.Invoke(v));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendMessage(string value)
        {
            await _connection.SendAsync("SendMessage", value);
        }

    }
}
