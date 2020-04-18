using System;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class SearchUiSystem : IEcsRunSystem {
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();
        EcsSonderGameWorld _world = null;
        EcsFilter<ItemSearchUi> _itemSearchUi = null;
        private Boolean isShown = false;
        public void Run() {
            if (_world.GameState.InventoryUiData != null && !isShown) {
                for (var i = 0; i < _itemSearchUi.EntitiesCount; i++) {
                    var pos = _camera.transform.position;
                    pos.z = 0;
                    _itemSearchUi.Components1[i].Body.Tr.localPosition = pos;
                    _itemSearchUi.Components1[i].Disabable.SetActive(true);
                    _itemSearchUi.Components1[i].SetHeader(_world.GameState.InventoryUiData.Storage.name);
                    for (var j = 0; j < _world.GameState.InventoryUiData.Storage.Inventory.Count; j++) {
                        _itemSearchUi.Components1[i].AddTextToContent(
                            _world, 
                            _world.GameState.InventoryUiData.Storage.Inventory[j].ToString()
                            );                        
                    }
                }

                isShown = true;
            } else if (_world.UiData.inventoryItemUi == null && isShown) {
                for (var i = 0; i < _itemSearchUi.EntitiesCount; i++) {
                    _itemSearchUi.Components1[i].Disabable.SetActive(false);
                    _itemSearchUi.Components1[i].ClearContent();
                }

                isShown = false;
            }
        }
    }
}