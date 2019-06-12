using System;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Entities;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class ObjectUseSystem : Delayed, IEcsRunSystem {
        EcsFilter<Human, ObjectUser> _humanUsers = null;

        public void Run() {
            if (CantUpdate()) return;

            for (var i = 0; i < _humanUsers.EntitiesCount; i++) {
                var user = _humanUsers.Components2[i];
                var human = _humanUsers.Components1[i];
                user.ObjectToUse = null;
                foreach (var usable in human.Movable.CurrentRoom.Usables) {
                    if (human.Body.Tr.position.x + human.Body.Size.x > usable.Body.Tr.position.x &&
                        human.Body.Tr.position.x < usable.Body.Tr.position.x + usable.Body.Size.x) {
                        user.ObjectToUse = usable;
                        break;
                    }
                }

                if (user.UsePressed) {
                    var usable = user.ObjectToUse;
                    if (usable != null) {
                        switch (usable.UsableType) {
                            case Usable.Type.Door:
                                if (!(usable.UsableObject is Door door)) return;
                                var newPos = door.Destination.Usable.Body.Tr.position;
                                var newRoom = door.Destination.Source;

                                newPos = new Vector3(newPos.x, newRoom.Floor, newPos.z);
                                human.Body.Tr.position = newPos;

                                human.TravelTo(newRoom);
                                break;
                            case Usable.Type.Box:
                                if (!(usable.UsableObject is Box box)) return;
                                if (box.Items.Count > 0) {
                                    var item = box.Items[0];
                                    box.Items.RemoveAt(0);
                                    human.Storage.Add(item);
                                    Debug.Log("Human got " + item + " from boxe. He now has " + human.Storage + " items");
                                }
                                else {
                                    Debug.Log("Human opened boxe but it's empty!");
                                }

                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

                user.UsePressed = false;
            }
        }
    }
}