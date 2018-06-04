using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class ObjectUseSystem : IEcsRunSystem
{
    EcsFilter<Human, ObjectUser> humanUsers = null;

    public void Run()
    {
        for (int i = 0; i < humanUsers.EntitiesCount; i++)
        {
            var user = humanUsers.Components2[i];
            var human = humanUsers.Components1[i];
            user.doorToUse = null;
            foreach (Door door in human.currentRoom.doors)
            {
                if (human.tr.position.x + human.size > door.tr.position.x && human.tr.position.x < door.tr.position.x + door.size)
                {
                    user.doorToUse = door;
                }
            }

            if (user.usePressed)
            {
                Door door = user.doorToUse;
                if (door != null)
                {
                    Vector3 newPos = door.destination.tr.position;
                    Room newRoom = door.destination.source;

                    newPos = new Vector3(newPos.x, newRoom.floor, newPos.z);
                    human.tr.position = newPos;

                    human.TravelTo(newRoom);
                }
            }
            user.usePressed = false;
        }
    }
}