﻿using System.Collections.Concurrent;

namespace Game.Application.Components
{
    public class WebSocketSessionCollection : ComponentBase, IWebSocketSessionCollection
    {
        private readonly ConcurrentDictionary<int, WebSocketSessionData> collection;

        public WebSocketSessionCollection()
        {
            collection = new ConcurrentDictionary<int, WebSocketSessionData>();
        }

        public bool Add(int id, WebSocketSessionData webSocketSessionData)
        {
            return collection.TryAdd(id, webSocketSessionData);
        }

        public bool Remove(int id)
        {
            return collection.TryRemove(id, out _);
        }

        public bool TryGet(int id, out WebSocketSessionData webSocketSessionData)
        {
            return collection.TryGetValue(id, out webSocketSessionData);
        }
    }
}