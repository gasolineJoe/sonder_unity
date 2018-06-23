using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class MoveProcessing : Delayed, IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<Human, Movable> movableHumans = null;

    public void Initialize()
    {
        _delay = 0.03f;
    }

    public void Run()
    {
        if (CantUpdate()) return;

        for (int i = 0; i < movableHumans.EntitiesCount; i++)
        {
            var movable = movableHumans.Components2[i];
            var human = movableHumans.Components1[i];
            var tr = human.tr;
            float movementX = movable.acceleration * movable.speed * _delay;
            float rightEdge = tr.localPosition.x + movementX + human.size;
            float roomSize = human.currentRoom.size;
            if (rightEdge < roomSize && tr.localPosition.x + movementX > 0)
                tr.Translate(movementX, 0, 0);
            else
            {
                if (rightEdge < roomSize)
                {
                    tr.Translate(-tr.localPosition.x, 0, 0);
                }
                else
                {
                    tr.Translate(roomSize- tr.localPosition.x - human.size, 0, 0);
                }
            }

        }
    }

    public void Destroy()
    {
    }
}
