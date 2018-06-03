using LeopotamGroup.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    public SonderStartupData assets;

    EcsSonderGameWorld _world; EcsSystems _systems; void OnEnable()
    {
        _world = new EcsSonderGameWorld(assets);
#if UNITY_EDITOR
        LeopotamGroup.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
#endif
        _systems = new EcsSystems(_world)
            .Add(new UserInputProcessing())
            .Add(new MoveProcessing())
            .Add(new SpawnSystem());
        _systems.Initialize();
#if UNITY_EDITOR
        LeopotamGroup.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
    }

    void Update() { _systems.Run(); }

    void OnDisable() { _systems.Destroy(); }
}