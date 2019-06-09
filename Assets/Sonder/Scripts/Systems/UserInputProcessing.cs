using Sonder.Scripts.Components.Abilities;

namespace Sonder.Scripts.Systems {
    using System;
    using LeopotamGroup.Ecs;
    using UnityEngine;

    [EcsInject]
    public class UserInputProcessing : IEcsRunSystem {
        EcsFilter<Movable, InputControlled> _controlledEntities = null;
        EcsFilter<ObjectUser, InputControlled> _controlledUsers = null;
        EcsFilter<Storage, InputControlled> _controlledStorages = null;

        public void Run() {
        }
    }
}