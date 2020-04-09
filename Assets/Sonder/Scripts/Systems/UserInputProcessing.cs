﻿using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts.Mind;
using Sonder.Scripts.Components.World.Entities;
using Sonder.Scripts.Ui;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserInputProcessing : IEcsRunSystem {
        EcsFilter<Human, ActionQueue, InputControlled> _controlledEntities = null;
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();
        EcsSonderGameWorld _world = null;

        public void Run() {
            if (Input.GetMouseButtonDown(0)) {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < _controlledEntities.EntitiesCount; i++) {
                    var human = _controlledEntities.Components1[i];
                    var point = human.Movable.WorldPosition.Room.Body.Tr.InverseTransformPoint(worldPoint);
                    for (var j = 0; j < human.Movable.WorldPosition.Room.Usables.Count; j++) {
                        var usable = human.Movable.WorldPosition.Room.Usables[j];
                        if (usable.Body.isInBounds(point)) {
                            human.ActionQueue.Interrupt();
                            human.ActionQueue.AddWalk(usable.Body.Tr.localPosition.x + usable.Body.Size.x / 2);
                            human.ActionQueue.AddUse(usable);
                            return;
                        }
                    }

                    human.ActionQueue.Interrupt();
                    human.ActionQueue.AddWalk(point.x);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (_world.SystemUiMode == SystemUiMode.NONE) {
                    _world.IsFrozen = true;
                    _world.SystemUiMode = SystemUiMode.PAUSE;
                }
                else if (_world.SystemUiMode == SystemUiMode.PAUSE) {
                    _world.IsFrozen = false;
                    _world.SystemUiMode = SystemUiMode.NONE;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (_world.SystemUiMode == SystemUiMode.NONE) {
                    _world.IsFrozen = true;
                    _world.SystemUiMode = SystemUiMode.MENU;
                }
                else if (_world.SystemUiMode == SystemUiMode.MENU) {
                    _world.IsFrozen = false;
                    _world.SystemUiMode = SystemUiMode.NONE;
                }
            } 
        }
    }
}