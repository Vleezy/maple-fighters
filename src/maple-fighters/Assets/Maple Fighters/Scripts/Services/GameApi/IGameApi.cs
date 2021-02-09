using System;
using Game.Messages;

namespace Scripts.Services.GameApi
{
    public interface IGameApi
    {
        Action<EnteredSceneMessage> SceneEntered { get; set; }

        Action<SceneChangedMessage> SceneChanged { get; set; }

        Action<GameObjectsAddedMessage> GameObjectsAdded { get; set; }

        Action<GameObjectsRemovedMessage> GameObjectsRemoved { get; set; }

        Action<PositionChangedMessage> PositionChanged { get; set; }

        Action<AnimationStateChangedMessage> AnimationStateChanged { get; set; }

        Action<AttackedMessage> Attacked { get; set; }

        Action<BubbleNotificationMessage> BubbleMessageReceived { get; set; }

        void SendMessage<T, M>(T code, M message)
            where T : IComparable, IFormattable, IConvertible
            where M : struct;
    }
}