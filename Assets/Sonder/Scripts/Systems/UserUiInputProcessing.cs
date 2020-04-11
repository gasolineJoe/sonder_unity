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
            if (_world.SystemUiMode == SystemUiMode.NONE) {
                _world.IsFrozen = true;
                _world.SystemUiMode = mode;
                if (_world.SystemUiMode == SystemUiMode.PAUSE)
                    ShowSearchUI(mode.ToString());
                else
                    ShowUiMessage(mode.ToString());
            }
            else if (_world.SystemUiMode == mode) {
                if (_world.SystemUiMode == SystemUiMode.PAUSE)
                    HideSearchUI();
                else
                    HideUiMessage();
                _world.IsFrozen = false;
                _world.SystemUiMode = SystemUiMode.NONE;
            }
        }

        private void ShowUiMessage(String text) {
            for (var i = 0; i < _uiComponent.EntitiesCount; i++) {
                var pos = _camera.transform.position;
                pos.z = -5;
                _uiComponent.Components1[i].Body.Tr.position = pos;
                _uiComponent.Components1[i].Disabable.SetActive(true);
                _uiComponent.Components1[i].SetText(text);
            }
        }

        private void HideUiMessage() {
            for (var i = 0; i < _uiComponent.EntitiesCount; i++) {
                _uiComponent.Components1[i].Disabable.SetActive(false);
            }
        }

        private void ShowSearchUI(String text) {
            for (var i = 0; i < _itemSearchUi.EntitiesCount; i++) {
                var pos = _camera.transform.position;
                pos.z = -5;
                _itemSearchUi.Components1[i].Body.Tr.position = pos;
                _itemSearchUi.Components1[i].Disabable.SetActive(true);
                _itemSearchUi.Components1[i].SetHeader(text);
                _itemSearchUi.Components1[i].AddTextToContent(_world, "some item 1");
                _itemSearchUi.Components1[i].AddTextToContent(_world, "some item 2");
                _itemSearchUi.Components1[i].AddTextToContent(_world, "some item 3");
            }
        }

        private void HideSearchUI() {
            for (var i = 0; i < _itemSearchUi.EntitiesCount; i++) {
                _itemSearchUi.Components1[i].Disabable.SetActive(false);
                _itemSearchUi.Components1[i].ClearContent();
            }
        }
    }
}