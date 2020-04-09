using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UiSpawnSystem : IEcsInitSystem {
        EcsSonderGameWorld _world = null;
        private GameObject _ui;

        public void Initialize() {
            _ui = _world.UiData.uiText;
            var newUiObject = Spawn(_ui);
            UiComponent.New(_world, newUiObject);
        }
        
        private GameObject Spawn(GameObject gameObject) {
            return Object.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
        }

        public void Destroy() {
            
        }
    }
}