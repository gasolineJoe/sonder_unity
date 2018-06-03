using UnityEngine;
using LeopotamGroup.Ecs;

[EcsInject]
public class UserInputProcessing : IEcsRunSystem
{
    EcsWorld _world = null;
    EcsFilter<Movable, InputControlled> controlledEntities = null;

    float xAxis, yAxis;

    public void Run()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        for (int i = 0; i < controlledEntities.EntitiesCount; i++)
        {
            controlledEntities.Components1[i].acceleration = xAxis;
        }
    }
}