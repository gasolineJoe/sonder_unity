using LeopotamGroup.Ecs;
using Sonder.Scripts.Ui;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UiSystem : Delayed, IEcsRunSystem {
        EcsSonderGameWorld _world = null;
        EcsFilter<UiComponent> _uiComponents = null;
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();
        public void Run() {
            if (CantUpdate()) return;

            if (_world.SystemUiMode == SystemUiMode.PAUSE) {
                for (var i = 0; i < _uiComponents.EntitiesCount; i++) {
                    var pos = _camera.transform.position;
                    pos.z = -5;
                    _uiComponents.Components1[i].Body.Tr.position = pos;
                    _uiComponents.Components1[i].Disabable.SetActive(true);
                }
            } else if (_world.SystemUiMode == SystemUiMode.NONE) {
                for (var i = 0; i < _uiComponents.EntitiesCount; i++) {
                    _uiComponents.Components1[i].Disabable.SetActive(false);
                }
            }
        }
    }
}