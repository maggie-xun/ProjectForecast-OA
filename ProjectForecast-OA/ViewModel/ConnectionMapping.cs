using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.ViewModel
{
    public class ConnectionMapping<T>
    {
        protected readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public IReadOnlyDictionary<T, HashSet<string>> Get()
        {
            return _connections;
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public IEnumerable<T> GetKeys()
        {
            return _connections.Keys;
        }

        public T GetKey(string connectionId)
        {
            return _connections.FirstOrDefault(x => x.Value.Contains(connectionId)).Key;
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }

        public void RemoveConnection(string connectionId)
        {
            lock (_connections)
            {
                T removeKey = default(T);
                foreach (var user in _connections.Keys)
                {
                    var userConnections = _connections[user];

                    var currentConnection = userConnections.FirstOrDefault(x => x == connectionId);

                    if (currentConnection != null)
                    {
                        lock (userConnections)
                        {
                            userConnections.Remove(currentConnection);

                            if (userConnections.Count == 0)
                            {
                                removeKey = user;
                            }

                            break;
                        }
                    }
                }

                if (removeKey != null && !removeKey.Equals(default(T)))
                {
                    _connections.Remove(removeKey);
                }
            }
        }
    }
}