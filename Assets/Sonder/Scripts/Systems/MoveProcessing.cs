using System;
using System.Runtime.Remoting.Messaging;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using UnityEngine;
using Action = Sonder.Scripts.Components.Abilities.Mind.Action;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class MoveProcessing : Delayed, IEcsRunSystem, IEcsInitSystem {
        EcsFilter<ActionQueue, Movable> _movables = null;

        public void Initialize() {
            Delay = 0.03f;
        }

        public void Run() {
            if (CantUpdate()) return;
            for (var i = 0; i < _movables.EntitiesCount; i++) {
                var movable = _movables.Components2[i];
                var actionQueue = _movables.Components1[i];
                if (actionQueue.HasActions() && actionQueue.GetAction().Item1 == Action.Walk) {
                    Move(movable, actionQueue);
                }
            }
        }

        private void Move(Movable movable, ActionQueue actionQueue) {
            var tr = movable.Body.Tr;
            var to = actionQueue.GetAction().Item2;
            var xDelta = movable.CurrentSpeed * movable.MaxSpeed * Delay;
            var timeToStop = 1 / movable.Acceleration;
            var lengthToStop = movable.MaxSpeed / 1.82f * timeToStop;
            Debug.Log("time to stop " + timeToStop + "; length to stop = " + lengthToStop);
            movable.CurrentSpeed = GetSpeed(movable.CurrentSpeed, movable.Acceleration, tr.localPosition.x, to,
                lengthToStop, Delay);
            if (Math.Abs(movable.CurrentSpeed) < 0.01f) {
                actionQueue.ActionDone();
                Debug.Log(tr.localPosition.x);
                return;
            }

            TranslateWithBounds(tr, xDelta, movable.Body.Size, movable.CurrentRoom.Body.Size);
        }

        private void TranslateWithBounds(Transform tr, float xDelta, float trSize, float rightBound) {
            var rightEdge = tr.localPosition.x + xDelta + trSize;
            if (rightEdge < rightBound && tr.localPosition.x + xDelta > 0)
                tr.Translate(xDelta, 0, 0);
            else {
                if (rightEdge < rightBound)
                    tr.Translate(-tr.localPosition.x, 0, 0);
                else
                    tr.Translate(rightBound - tr.localPosition.x - trSize, 0, 0);
            }
        }

        private float GetSpeed(float currentSpeed, float acceleration, float position, float target, float closeRange,
            float delta) {
            var range = target - position;
            float newSpeed;
            if (Math.Abs(range) > closeRange) {
                newSpeed = currentSpeed + Math.Sign(range) * acceleration * delta;
            }
            else {
                newSpeed = currentSpeed - Math.Sign(range) * acceleration * delta;
                if (Math.Sign(newSpeed) != Math.Sign(currentSpeed) || Math.Sign(range) != Math.Sign(newSpeed))
                    newSpeed = 0;
            }

            return BoundValue(newSpeed, 1);
        }

        private float BoundValue(float value, float bound) {
            return value > bound ? bound : value < -bound ? -bound : value;
        }

        public void Destroy() { }
    }
}