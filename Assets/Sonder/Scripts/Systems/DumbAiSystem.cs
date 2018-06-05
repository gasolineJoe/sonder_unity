using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class DumbAiSystem : Delayed, IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<Human, Movable, ObjectUser>.Exclude<InputControlled> robots = null;

    public void Initialize()
    {
        _delay = 1;
    }

    public void Run()
    {
        if (CantUpdate()) return;
        for (int i = 0; i < robots.EntitiesCount; i++)
        {
            robots.Components2[i].acceleration = Random.Range(-1f, 1f);
            robots.Components3[i].usePressed = Random.Range(0, 2) == 1;
        }
    }

    public void Destroy()
    {
    }
}