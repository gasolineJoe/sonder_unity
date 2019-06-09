using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserInputProcessing : IEcsRunSystem {
        EcsFilter<Movable, InputControlled> _controlledEntities = null;
        EcsFilter<ObjectUser, InputControlled> _controlledUsers = null;
        EcsFilter<Storage, InputControlled> _controlledStorages = null;

        public void Run() {
        
        }
    }
}