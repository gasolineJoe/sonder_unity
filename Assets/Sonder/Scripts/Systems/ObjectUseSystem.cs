using LeopotamGroup.Ecs;
using Sonder.Scripts.Actions.Stuff;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts.Mind;
using Sonder.Scripts.Components.World.Entities;
using Sonder.Scripts.Components.World.Entities.Usables;
using Sonder.Scripts.Ui;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class ObjectUseSystem : BaseDelayedSystem, IEcsRunSystem {
        EcsFilter<Human, ActionQueue> _humanUsers = null;
        EcsSonderGameWorld _world = null;

        public void Run() {
            if (CantUpdate() || _world.IsFrozen) return;

            for (var i = 0; i < _humanUsers.EntitiesCount; i++) {
                var actionQueue = _humanUsers.Components2[i];
                var human = _humanUsers.Components1[i];
                if (!actionQueue.HasActions() || actionQueue.GetAction().Item1 != Action.Use) continue;
                if (!(actionQueue.GetAction().Item2 is Usable usable)) continue;
                switch (usable.UsableType) {
                    case Usable.Type.Box:
                        actionQueue.ActionDone();
                        if (human.InputControlled && _world.UiMode == UiMode.NONE) {
                            _world.IsFrozen = true;
                            _world.UiMode = UiMode.INVENTORY;
                        }
                        else if (!human.InputControlled) {
                            //todo human.ai.Search()
                        }

                        break;
                    case Usable.Type.Door:
                        if (!(usable.UsableEntity is Door door)) break;
                        var newPos = door.Destination.Usable.Body.Tr.position;
                        var oldPos = human.WorldPosition.Body.Tr.position;
                        var newRoom = door.Destination.Source;
                        var floorHeight = newRoom.Body.Tr.position.y + newRoom.Floor;
                        newPos = new Vector3(newPos.x, floorHeight, oldPos.z);
                        human.WorldPosition.Body.Tr.position = newPos;

                        TravelToRoom.Do(human, newRoom);
                        actionQueue.ActionDone();
                        break;
                    default:
                        actionQueue.ActionDone();
                        break;
                }
            }
        }
    }
}