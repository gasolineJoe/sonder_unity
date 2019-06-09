using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using Sonder.Scripts.Components.Entities;

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
                if (actionQueue.GetAction().Item1 == Action.Walk) {
                    //todo change acceleration according to target
                    var tr = movable.Body.Tr;
                    var movementX = movable.Acceleration * movable.Speed * Delay;
                    var rightEdge = tr.localPosition.x + movementX + movable.Body.Size;
                    var roomSize = movable.CurrentRoom.Body.Size;
                    if (rightEdge < roomSize && tr.localPosition.x + movementX > 0)
                        tr.Translate(movementX, 0, 0);
                    else {
                        if (rightEdge < roomSize) {
                            tr.Translate(-tr.localPosition.x, 0, 0);
                        }
                        else {
                            tr.Translate(roomSize - tr.localPosition.x - movable.Body.Size, 0, 0);
                        }
                    }
                }
            }
        }

        public void Destroy() {
        }
    }
}