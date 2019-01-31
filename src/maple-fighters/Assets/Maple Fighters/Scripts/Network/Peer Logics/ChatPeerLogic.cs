﻿using Chat.Common;
using CommonCommunicationInterfaces;

namespace Scripts.Services
{
    public sealed class ChatPeerLogic : PeerLogicBase, IChatPeerLogicAPI
    {
        public UnityEvent<ChatMessageEventParameters> ChatMessageReceived { get; }

        public ChatPeerLogic()
        {
            ChatMessageReceived = new UnityEvent<ChatMessageEventParameters>();
        }

        protected override void OnAwake()
        {
            base.OnAwake();

            ServerPeerHandler
                .SetEventHandler(ChatEvents.ChatMessage, ChatMessageReceived);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ServerPeerHandler.RemoveEventHandler(ChatEvents.ChatMessage);
        }

        public void SendChatMessage(ChatMessageRequestParameters parameters)
        {
            ServerPeerHandler
                .SendOperation(
                    ChatOperations.ChatMessage,
                    parameters,
                    MessageSendOptions.DefaultReliable());
        }
    }
}