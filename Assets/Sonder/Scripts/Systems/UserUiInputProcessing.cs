using System;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserUiInputProcessing : IEcsRunSystem {
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();
        EcsFilter<UiComponent> _uiComponents = null;
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
                ShowUI(mode.ToString());
            }
            else if (_world.SystemUiMode == mode) {
                _world.IsFrozen = false;
                _world.SystemUiMode = SystemUiMode.NONE;
                HideUI();
            }
        }
        
        private void ShowUI(String text){
            for (var i = 0; i < _uiComponents.EntitiesCount; i++) {
                var pos = _camera.transform.position;
                pos.z = -5;
                _uiComponents.Components1[i].Body.Tr.position = pos;
                _uiComponents.Components1[i].Disabable.SetActive(true);
                _uiComponents.Components1[i].SetText(text);
            }
        }

        private void HideUI() {
            for (var i = 0; i < _uiComponents.EntitiesCount; i++) {
                _uiComponents.Components1[i].Disabable.SetActive(false);
            }
        }
    }
}