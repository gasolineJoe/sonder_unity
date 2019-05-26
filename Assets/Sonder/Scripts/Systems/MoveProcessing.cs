using LeopotamGroup.Ecs;

[EcsInject]
public class MoveProcessing : Delayed, IEcsRunSystem, IEcsInitSystem {
    EcsFilter<Human, Movable> _movableHumans = null;

    public void Initialize() {
        Delay = 0.03f;
    }

    public void Run() {
        if (CantUpdate()) return;

        for (var i = 0; i < _movableHumans.EntitiesCount; i++) {
            var movable = _movableHumans.Components2[i];
            var human = _movableHumans.Components1[i];
            var tr = human.Tr;
            var movementX = movable.Acceleration * movable.Speed * Delay;
            var rightEdge = tr.localPosition.x + movementX + human.Size;
            var roomSize = human.CurrentRoom.Size;
            if (rightEdge < roomSize && tr.localPosition.x + movementX > 0)
                tr.Translate(movementX, 0, 0);
            else {
                if (rightEdge < roomSize) {
                    tr.Translate(-tr.localPosition.x, 0, 0);
                }
                else {
                    tr.Translate(roomSize - tr.localPosition.x - human.Size, 0, 0);
                }
            }
        }
    }

    public void Destroy() {
    }
}