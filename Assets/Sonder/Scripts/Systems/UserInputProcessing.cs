﻿using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using Sonder.Scripts.Components.Entities;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class UserInputProcessing : IEcsRunSystem {
        EcsFilter<Human, Movable, InputControlled> _controlledEntities = null;
        EcsFilter<ObjectUser, InputControlled> _controlledUsers = null;
        EcsFilter<Storage, InputControlled> _controlledStorages = null;
        private readonly Camera _camera = Object.FindObjectOfType<Camera>();

        public void Run() {
            if (Input.GetMouseButtonDown(0)) {
                var point = _camera.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < _controlledEntities.EntitiesCount; i++) {
                    var human = _controlledEntities.Components1[i];
                    human.ActionQueue.Interrupt();
                    human.ActionQueue.AddAction(Action.Walk, point.x);
                }
            }
        }
    }
}