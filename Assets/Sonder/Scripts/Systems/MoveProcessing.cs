using LeopotamGroup.Ecs;
using Sonder.Scripts.Actions.ActionExecutors;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts.Mind;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class MoveProcessing : Delayed, IEcsRunSystem, IEcsInitSystem {
        EcsFilter<ActionQueue, Movable> _movables = null;

        public void Initialize() {
            Delay = 0.03f;
        }

        public void Run() {
            if (CantUpdate()) return;
            for (var i = 0; i < _movables.EntitiesCount; i++) {
                var movable = _movables.Components2[i];
                var actionQueue = _movables.Components1[i];
                if (actionQueue.HasActions() && actionQueue.GetAction().Item1 == Action.Walk) {
                    Move.Do(movable, actionQueue, Delay);
                }
            }
        }

        public void Destroy() { }
    }
}