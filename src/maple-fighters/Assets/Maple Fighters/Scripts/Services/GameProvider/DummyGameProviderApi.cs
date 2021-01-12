using System;
using UnityEngine;

namespace Scripts.Services.GameProviderApi
{
    public class DummyGameProviderApi : MonoBehaviour, IGameProviderApi
    {
        public static DummyGameProviderApi GetInstance()
        {
            if (instance == null)
            {
                var gameProviderApi =
                    new GameObject("Dummy GameProvider Api");
                instance =
                    gameProviderApi.AddComponent<DummyGameProviderApi>();
            }

            return instance;
        }

        private static DummyGameProviderApi instance;

        public Action<long, string> GetGamesCallback { get; set; }

        public void GetGames()
        {
            var statusCode = 200;
            var json = "[{\"name\":\"Game 1\",\"ip\":\"127.0.0.1\",\"port\":50051}]";

            GetGamesCallback?.Invoke(statusCode, json);
        }
    }
}