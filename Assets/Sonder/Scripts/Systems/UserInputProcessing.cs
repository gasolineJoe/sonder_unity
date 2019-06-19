using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts.Mind;
using Sonder.Scripts.Components.World.Entities;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserInputProcessing : IEcsRunSystem {
        EcsFilter<Human, ActionQueue, InputControlled> _controlledEntities = null;
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();

        public void Run() {
            if (Input.GetMouseButtonDown(0)) {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < _controlledEntities.EntitiesCount; i++) {
                    var human = _controlledEntities.Components1[i];
                    var point = human.Movable.WorldPosition.Room.Body.Tr.InverseTransformPoint(worldPoint);
                    for (var j = 0; j < human.Movable.WorldPosition.Room.Usables.Count; j++) {
                        var usable = human.Movable.WorldPosition.Room.Usables[j];
                        if (usable.Body.isInBounds(point)) {
                            human.ActionQueue.Interrupt();
                            human.ActionQueue.AddWalk(usable.Body.Tr.localPosition.x + usable.Body.Size.x / 2);
                            human.ActionQueue.AddUse(usable);
                            return;
                        }
                    }

                    human.ActionQueue.Interrupt();
                    human.ActionQueue.AddWalk(point.x);
                }
            }
        }
    }
}