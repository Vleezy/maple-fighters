﻿using System.Collections.Generic;
using Common.ComponentModel;

namespace Game.Application.Components
{
    public class GameSceneCollection : ComponentBase, IGameSceneCollection
    {
        private readonly Dictionary<Map, IGameScene> collection;

        public GameSceneCollection()
        {
            collection = new Dictionary<Map, IGameScene>();
        }

        public bool Add(Map map, IGameScene gameScene)
        {
            return collection.TryAdd(map, gameScene);
        }

        public bool Remove(Map map)
        {
            return collection.TryRemove(map, out _);
        }

        public bool TryGet(Map map, out IGameScene gameScene)
        {
            return collection.TryGetValue(map, out gameScene);
        }
    }
}