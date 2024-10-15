using Karma_Backend.Models;
using Microsoft.AspNetCore.SignalR;
namespace Karma_Backend.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IDictionary<string, UserRoomConnection> _connections;
        public ChatHub(IDictionary<string, UserRoomConnection> connection)
        {
            _connections = connection;
        }
        public async Task JoinRoom(UserRoomConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room!);
            _connections[Context.ConnectionId] = userConnection;
            await Clients.Group(userConnection.Room!)
                .SendAsync(method: "ReceiveMessage", arg1: "Lets Chat!", arg2: $"{userConnection.User} has Joined the Group", arg3: DateTime.Now);
            await SendConnectedUser(userConnection.Room!);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
            {
                await Clients.Group(userRoomConnection.Room!)
                    .SendAsync(method: "ReceiveMessage", arg1: userRoomConnection.User, arg2: message, arg3: DateTime.Now);
            }
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (!_connections.TryGetValue(Context.ConnectionId, out UserRoomConnection roomConnection))
            {
                return base.OnDisconnectedAsync(exception);
            }
            _connections.Remove(Context.ConnectionId);
            Clients.Group(roomConnection.Room!)
                 .SendAsync(method: "ReceiveMessage", arg1: "Lets Chat!", arg2: $"{roomConnection.User} has Left the Group", arg3: DateTime.Now);
            SendConnectedUser(roomConnection.Room!);
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendConnectedUser(string room)
        {
            var users = _connections.Values
                .Where(u => u.Room == room)
                .Select(s => s.User);
            return Clients.Group(room).SendAsync("ConnectedUser", users);
        }
    }
}
