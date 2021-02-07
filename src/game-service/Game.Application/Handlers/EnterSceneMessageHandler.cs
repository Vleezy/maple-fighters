using Game.Application.Components;
using Game.Messages;
using Game.Network;
using Game.Application.Objects;
using Game.Application.Objects.Components;
using Common.MathematicsHelper;

namespace Game.Application.Handlers
{
    public class EnterSceneMessageHandler : IMessageHandler<EnterSceneMessage>
    {
        private readonly IGameObject player;
        private readonly ICharacterData characterData;
        private readonly IPresenceMapProvider presenceMapProvider;
        private readonly IMessageSender messageSender;
        private readonly IGameSceneCollection gameSceneCollection;

        public EnterSceneMessageHandler(IGameObject player, IGameSceneCollection gameSceneCollection)
        {
            this.player = player;
            this.gameSceneCollection = gameSceneCollection;

            characterData = player.Components.Get<ICharacterData>();
            presenceMapProvider = player.Components.Get<IPresenceMapProvider>();
            messageSender = player.Components.Get<IMessageSender>();
        }

        public void Handle(EnterSceneMessage message)
        {
            var map = message.Map;
            var name = message.CharacterName;
            var characterType = message.CharacterType;

            characterData.SetName(name);
            characterData.SetCharacterType(characterType);

            if (gameSceneCollection.TryGet((Map)map, out var gameScene))
            {
                // Set's player position
                var position = gameScene.GamePlayerSpawnData.GetPosition();
                var direction = gameScene.GamePlayerSpawnData.GetDirection();

                player.Transform.SetPosition(position);
                player.Transform.SetSize(new Vector2(direction, y: 0));

                // Set's map
                presenceMapProvider.SetMap(gameScene);

                // Adds to this game scene
                var playerGameObject = player as PlayerGameObject;
                if (playerGameObject != null)
                {
                    var bodyData = playerGameObject.CreateBodyData();

                    gameScene.GameObjectCollection.Add(player);
                    gameScene.PhysicsWorldManager.AddBody(bodyData);
                }
            }

            SendEnteredSceneMessage();
        }

        private void SendEnteredSceneMessage()
        {
            var messageCode = (byte)MessageCodes.EnteredScene;
            var message = new EnteredSceneMessage()
            {
                GameObjectId = player.Id,
                X = player.Transform.Position.X,
                Y = player.Transform.Position.Y,
                Direction = player.Transform.Size.X,
                CharacterName = characterData.GetName(),
                CharacterClass = characterData.GetCharacterType()
            };

            messageSender.SendMessage(messageCode, message);
        }
    }
}