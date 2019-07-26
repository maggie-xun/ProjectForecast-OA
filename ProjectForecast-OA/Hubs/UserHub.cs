using Microsoft.AspNet.SignalR;
using ProjectForecast_OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectForecast_OA.Hubs
{
    public class UserHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public override async Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            messaging($"Connected: {name} - {Context.ConnectionId}");

            _connections.Add(name, Context.ConnectionId);

            broadcastUsers();

            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var name = _connections.GetKey(Context.ConnectionId);

            messaging($"Disconnected ({stopCalled}):  {Context.ConnectionId}");

            _connections.RemoveConnection(Context.ConnectionId);

            broadcastUsers();

            await base.OnDisconnected(stopCalled);
        }

        public override async Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            messaging($"Reconnected: {name} - {Context.ConnectionId}");

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            broadcastUsers();

            await base.OnReconnected();
        }

        public string Login()
        {
            string name = Context.User.Identity.Name;

            return name;
        }

        public IEnumerable<string> Trace()
        {
            var userList = _connections.Get().Select(c => $"{c.Key} ({c.Value.Count})");

            return userList;
        }

        private void broadcastUsers()
        {
            var userList = _connections.Get().Select(c => $"{c.Key} ({c.Value.Count})");

            Clients.All.tracing(userList);
        }

        private void messaging(string message)
        {
            Clients.Caller.messaging(message);
        }
    }
}