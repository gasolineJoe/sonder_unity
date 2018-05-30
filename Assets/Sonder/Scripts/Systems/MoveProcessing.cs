using LeopotamGroup.Ecs;
using UnityEngine;
using UnityEngine.UI;

[EcsInject]
public class MoveProcessing : IEcsInitSystem, IEcsRunSystem
{
    EcsWorld _world = null;

    public void Initialize()
    {
    }

    public void Run()
    {
    }

    public void Destroy()
    {
    }
}
