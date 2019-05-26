﻿using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class ObjectUseSystem : Delayed, IEcsRunSystem {
    EcsFilter<Human, ObjectUser> _humanUsers = null;

    public void Run() {
        if (CantUpdate()) return;

        for (var i = 0; i < _humanUsers.EntitiesCount; i++) {
            var user = _humanUsers.Components2[i];
            var human = _humanUsers.Components1[i];
            user.DoorToUse = null;
            foreach (var door in human.CurrentRoom.Doors) {
                if (human.Tr.position.x + human.Size > door.Tr.position.x &&
                    human.Tr.position.x < door.Tr.position.x + door.Size) {
                    user.DoorToUse = door;
                }
            }

            if (user.UsePressed) {
                var door = user.DoorToUse;
                if (door != null) {
                    var newPos = door.Destination.Tr.position;
                    var newRoom = door.Destination.Source;

                    newPos = new Vector3(newPos.x, newRoom.Floor, newPos.z);
                    human.Tr.position = newPos;

                    human.TravelTo(newRoom);
                }
            }

            user.UsePressed = false;
        }
    }
}