using System;
using DefaultNamespace;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using Sonder.Scripts.Components.Entities;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class DumbAiSystem : Delayed, IEcsRunSystem, IEcsInitSystem {
        EcsFilter<Human, ActionQueue>.Exclude<InputControlled> _robots = null;
        EcsFilter<Room> _rooms;

        public void Initialize() {
            Delay = 1;
        }

        public void Run() {
            if (CantUpdate()) return;
            var random = new Random();
            for (var i = 0; i < _robots.EntitiesCount; i++) {
                var human = _robots.Components1[i];
                if (!human.ActionQueue.HasActions()) {
                    MakePath.Do(human, _rooms.Components1[random.Next(_rooms.EntitiesCount)]);
                }
            }
        }

        public void Destroy() { }
    }
}