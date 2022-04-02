using Game.Application.Components;

namespace Game.Application.Objects
{
    public class GameObjectGetter : ComponentBase, IGameObjectGetter
    {
        private readonly IGameObject gameObject;

        public GameObjectGetter(IGameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public IGameObject Get()
        {
            return gameObject;
        }
    }
}