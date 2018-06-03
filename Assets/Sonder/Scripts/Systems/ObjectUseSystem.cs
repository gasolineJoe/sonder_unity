using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class ObjectUseSystem : IEcsRunSystem
{
    EcsWorld _world = null;
    EcsFilter<Human, ObjectUser> humanUsers = null;

    public void Run()
    {
        for (int i = 0; i < humanUsers.EntitiesCount; i++)
        {
            var user = humanUsers.Components2[i];
            var human = humanUsers.Components1[i];
            user.doorToUse = null;
            foreach (DoorComponent door in human.currentRoom.doors)
            {
                if (human.tr.position.x + human.size > door.tr.position.x && human.tr.position.x < door.tr.position.x + door.size)
                {
                    user.doorToUse = door;
                }
            }

            if (user.usePressed)
            {
                DoorComponent door = user.doorToUse;
                if (door != null)
                {
                    Vector3 newPos = door.destination.tr.position;
                    RoomComponent newRoom = door.destination.source;

                    newPos = new Vector3(newPos.x, newRoom.floor, newPos.z);
                    human.tr.position = newPos;

                    //traveller.TravelTo(newRoom);
                }
            }
            user.usePressed = false;
        }
    }
}