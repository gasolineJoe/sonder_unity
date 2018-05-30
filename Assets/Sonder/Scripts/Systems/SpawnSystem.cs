using LeopotamGroup.Ecs;
using UnityEngine;
using UnityEngine.UI;

[EcsInject]
public class SpawnSystem : IEcsInitSystem
{
    EcsWorld _world = null;

    public void Initialize()
    {

    }

    public void Destroy()
    {
    }
}
