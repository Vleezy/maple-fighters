using System.Collections.Generic;
using Game.Application.Components;

namespace Game.Application.Objects.Components
{
    public class MobBehaviourManager : ComponentBase
    {
        private readonly Dictionary<MobBehaviour, IMobBehaviour> behaviours;

        protected override void OnAwake()
        {
            var mobMoveBehaviour = Components.Get<IMobMoveBehaviour>();
            behaviours.Add(MobBehaviour.Move, mobMoveBehaviour);
        }
    }
}