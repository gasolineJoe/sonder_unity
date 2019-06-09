using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Entities;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class DumbAiSystem : Delayed, IEcsRunSystem, IEcsInitSystem {
        EcsFilter<Human, Movable, ObjectUser>.Exclude<InputControlled> _robots = null;

        public void Initialize() {
            Delay = 1;
        }

        public void Run() {
            if (CantUpdate()) return;
        
        }

        public void Destroy() {
        }
    }
}