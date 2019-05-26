using System;
using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class ObjectUseSystem : Delayed, IEcsRunSystem {
    EcsFilter<Human, ObjectUser> _humanUsers = null;

    public void Run() {
        if (CantUpdate()) return;

        for (var i = 0; i < _humanUsers.EntitiesCount; i++) {
            var user = _humanUsers.Components2[i];
            var human = _humanUsers.Components1[i];
            user.ObjectToUse = null;
            foreach (var usable in human.CurrentRoom.Usables) {
                if (human.Tr.position.x + human.Size > usable.Tr.position.x &&
                    human.Tr.position.x < usable.Tr.position.x + usable.Size) {
                    user.ObjectToUse = usable;
                }
            }

            if (user.UsePressed) {
                var usable = user.ObjectToUse;
                if (usable != null) {
                    switch (usable.UsableType) {
                        case Usable.Type.Door:
                            if (!(usable.UsableObject is Door door)) return;
                            var newPos = door.Destination.Usable.Tr.position;
                            var newRoom = door.Destination.Source;

                            newPos = new Vector3(newPos.x, newRoom.Floor, newPos.z);
                            human.Tr.position = newPos;

                            human.TravelTo(newRoom);
                            break;
                        case Usable.Type.Box:
                            Debug.Log("human " + human.Entity + " is using boxe");
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