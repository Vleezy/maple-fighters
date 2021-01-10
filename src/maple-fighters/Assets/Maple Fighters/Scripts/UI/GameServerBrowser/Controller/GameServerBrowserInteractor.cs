﻿using Scripts.Services.GameProviderApi;
using UnityEngine;

namespace Scripts.UI.GameServerBrowser
{
    [RequireComponent(typeof(IOnGameServerReceivedListener))]
    public class GameServerBrowserInteractor : MonoBehaviour
    {
        private IGameProviderApi gameProviderApi;
        private IOnGameServerReceivedListener onGameServerReceivedListener;

        private void Awake()
        {
            gameProviderApi =
                FindObjectOfType<GameProviderApi>();
            onGameServerReceivedListener =
                GetComponent<IOnGameServerReceivedListener>();
        }

        public void SetGameServerInfo(string ip, int port)
        {
            // TODO: Set game server data
        }

        public void ProvideGameServers()
        {
            var data = new[]
            {
                new UIGameServerButtonData(
                    ip: "localhost",
                    serverName: "Game 1",
                    port: 1000,
                    connections: 0,
                    maxConnections: 1000)
            };

            gameProviderApi?.ProvideGameServer();

            onGameServerReceivedListener.OnGameServerReceived(data);
        }
    }
}