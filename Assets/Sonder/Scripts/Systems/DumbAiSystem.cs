using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class DumbAiSystem : Delayed, IEcsRunSystem, IEcsInitSystem {
    EcsFilter<Human, Movable, ObjectUser>.Exclude<InputControlled> _robots = null;

    public void Initialize() {
        Delay = 1;
    }

    public void Run() {
        if (CantUpdate()) return;
        for (int i = 0; i < _robots.EntitiesCount; i++) {
            _robots.Components2[i].Acceleration = Random.Range(-1f, 1f);
            _robots.Components3[i].UsePressed = Random.Range(0, 2) == 1;
        }
    }

    public void Destroy() {
    }
}