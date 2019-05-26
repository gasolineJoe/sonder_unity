using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class UserInputProcessing : IEcsRunSystem {
    EcsFilter<Movable, InputControlled> _controlledEntities = null;
    EcsFilter<ObjectUser, InputControlled> _controlledUsers = null;

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
    }
}