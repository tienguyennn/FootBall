using Microsoft.AspNetCore.SignalR;

namespace N.Core.SignalR.Hubs
{
    public class ChatHub : Hub
    {

        private static readonly List<string> _connectionIds = new List<string>();
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            _connectionIds.Add(Context?.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connectionIds.Remove(Context?.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
