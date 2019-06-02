using System;
using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class UserInputProcessing : IEcsRunSystem {
    EcsFilter<Movable, InputControlled> _controlledEntities = null;
    EcsFilter<ObjectUser, InputControlled> _controlledUsers = null;
    EcsFilter<Storage, InputControlled> _controlledStorages = null;

    float _xAxis;

    public void Run() {
        _xAxis = Input.GetAxis("Horizontal");
        for (var i = 0; i < _controlledEntities.EntitiesCount; i++) {
            _controlledEntities.Components1[i].Acceleration = _xAxis;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            for (var i = 0; i < _controlledUsers.EntitiesCount; i++) {
                _controlledUsers.Components1[i].UsePressed = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            for (var i = 0; i < _controlledStorages.EntitiesCount; i++) {
                String result = "inventory: ";
                for (var j = 0; j < _controlledStorages.Components1[i].Inventory.Count; j++) {
                    result += _controlledStorages.Components1[i].Inventory[j] + " ";
                }

                if (_controlledStorages.Components1[i].Inventory.Count == 0) {
                    result += "empty";
                }
                Debug.Log(result);
            }
        }
    }
}