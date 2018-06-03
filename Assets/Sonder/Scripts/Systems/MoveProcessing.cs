using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class MoveProcessing : IEcsRunSystem
{
    EcsWorld _world = null;
    EcsFilter<Human, Movable> movableHumans = null;

    public void Run()
    {
        for (int i = 0; i < movableHumans.EntitiesCount; i++)
        {
            var movable = movableHumans.Components2[i];
            var tr = movableHumans.Components1[i].tr;
            float movementX = movable.acceleration * movable.speed;
            float position = tr.localPosition.x + movementX;
            // + GetComponent<Collider2D>().bounds.size.x;
            //float roomSize = GetComponent<RoomTraveller>().currentRoom.GetComponent<Collider2D>().bounds.size.x;
            //if (position < roomSize && transform.localPosition.x + movementX > 0)
            tr.position = new Vector3(tr.position.x + movementX, tr.position.y, tr.position.z);
        }
    }
}
