using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UiSpawnSystem : IEcsInitSystem {
        EcsSonderGameWorld _world = null;
        private GameObject _ui;

        public void Initialize() {
            var newUiObject = ObjectSpawnExtensions.SpawnUi(_world.UiData.uiText);
            TextMessageUi.New(_world, newUiObject);
            var searchUiObject = ObjectSpawnExtensions.SpawnUi(_world.UiData.containerSearchUi);
            ItemSearchUi.New(_world, searchUiObject, _world.UiData.inventoryItemUi);
        }

        public void Destroy() {
            
        }
    }
}