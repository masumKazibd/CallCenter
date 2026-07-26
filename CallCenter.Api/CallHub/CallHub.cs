using Microsoft.AspNetCore.SignalR;

namespace CallCenter.Api.CallHub
{
    public class CallHub : Hub
    { 
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"✅ Client Connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
         
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"❌ Client Disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
