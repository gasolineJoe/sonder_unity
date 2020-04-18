using System;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserUiInputProcessing : IEcsRunSystem {
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();
        EcsFilter<ItemSearchUi> _itemSearchUi = null;
        private EcsFilter<TextMessageUi> _uiComponent = null;
        EcsSonderGameWorld _world = null;

        public void Run() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SwitchWorldState(SystemUiMode.PAUSE);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                SwitchWorldState(SystemUiMode.MENU);
            }
        }

        private void SwitchWorldState(SystemUiMode mode) {
            if (_world.GameState.SystemUiMode == SystemUiMode.NONE) {
                _world.GameState.IsFrozen = true;
                _world.GameState.SystemUiMode = mode;
                ShowUiMessage(mode.ToString());
            }
            else if (_world.GameState.SystemUiMode == mode) {
                HideUiMessage();
                _world.GameState.IsFrozen = false;
                _world.GameState.SystemUiMode = SystemUiMode.NONE;
            }
        }

        private void ShowUiMessage(String text) {
            for (var i = 0; i < _uiComponent.EntitiesCount; i++) {
                var pos = _camera.transform.position;
                pos.z = 0;
                _uiComponent.Components1[i].Body.Tr.localPosition = pos;
                _uiComponent.Components1[i].Disabable.SetActive(true);
                _uiComponent.Components1[i].SetText(text);
            }
        }

        private void HideUiMessage() {
            for (var i = 0; i < _uiComponent.EntitiesCount; i++) {
                _uiComponent.Components1[i].Disabable.SetActive(false);
            }
        }
    }
}