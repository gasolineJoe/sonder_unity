using LeopotamGroup.Ecs;
using LeopotamGroup.Ecs.UnityIntegration;
using Sonder.Scripts.AssetHandlers;
using Sonder.Scripts.Systems;
using UnityEngine;

namespace Sonder.Scripts {
    public class GameStartup : MonoBehaviour {
        public SonderAssetData assets;
        public SonderUiData uiAssets;

        EcsSonderGameWorld _world;
        EcsSystems _systems;

        void OnEnable() {
            _world = new EcsSonderGameWorld(assets, uiAssets);
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
#endif
            _systems = new EcsSystems(_world)
                    .Add(new SpawnSystem())
                    .Add(new UserInputProcessing())
                    .Add(new MoveProcessing())
                    .Add(new ObjectUseSystem())
                    .Add(new DumbAiSystem())
                ;
            _systems.Initialize();
#if UNITY_EDITOR
            EcsSystemsObserver.Create(_systems);
#endif
        }

        void Update() {
            _systems.Run();
        }

        void OnDisable() {
            _systems.Destroy();
        }
    }
}