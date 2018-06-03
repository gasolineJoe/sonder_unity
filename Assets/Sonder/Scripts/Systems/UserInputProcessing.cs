using UnityEngine;
using LeopotamGroup.Ecs;

[EcsInject]
public class UserInputProcessing : IEcsRunSystem
{
    EcsWorld _world = null;
    EcsFilter<Movable, InputControlled> controlledEntities = null;
    EcsFilter<ObjectUser, InputControlled> controlledUsers = null;

    float xAxis, yAxis;

    public void Run()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        for (int i = 0; i < controlledEntities.EntitiesCount; i++)
        {
            controlledEntities.Components1[i].acceleration = xAxis;
        }

        if (Input.GetKeyDown(KeyCode.F))
            for (int i = 0; i < controlledUsers.EntitiesCount; i++)
            {
                controlledUsers.Components1[i].usePressed = true;
            }
    }
}