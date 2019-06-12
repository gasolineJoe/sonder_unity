﻿using System;
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
            var to = FixTarget(actionQueue.GetAction().Item2, movable);
            var currentPosition = tr.localPosition.x + movable.Body.Size/2;
            var xDelta = movable.CurrentSpeed * movable.MaxSpeed * Delay;
            SetLengthToStop(currentPosition, movable, to);
            SetSpeed(movable, currentPosition, to, Delay);
            TranslateWithBounds(movable, tr, xDelta, movable.Body.Size, movable.CurrentRoom.Body.Size);
            CheckDone(movable, actionQueue);
        }

        private float FixTarget(float target, Movable movable) {
            if (target < 0) return 0;
            if (target > movable.CurrentRoom.Body.Size) return movable.CurrentRoom.Body.Size - 1;
            return target;
        }

        private void CheckDone(Movable movable, ActionQueue queue) {
            if (Math.Abs(movable.CurrentSpeed) < 0.01f) {
                queue.ActionDone();
                movable.LengthToStop = 0;
            }
        }

        private void SetLengthToStop(float position, Movable movable, float target) {
            if (Math.Abs(movable.LengthToStop) < 0.01f) {
                var timeToStop = 1 / movable.Acceleration;
                movable.LengthToStop = movable.MaxSpeed / 1.82f * timeToStop;
                if (movable.LengthToStop * 2 > Math.Abs(target - position))
                    movable.LengthToStop = Math.Abs(target - position) / 2;
            }
        }

        private void TranslateWithBounds(Movable movable, Transform tr, float xDelta, float trSize, float rightBound) {
            var rightEdge = tr.localPosition.x + xDelta + trSize;
            if (rightEdge < rightBound && tr.localPosition.x + xDelta > 0)
                tr.Translate(xDelta, 0, 0);
            else {
                movable.CurrentSpeed = movable.CurrentSpeed / 2;
                if (rightEdge < rightBound)
                    tr.Translate(-tr.localPosition.x, 0, 0);
                else
                    tr.Translate(rightBound - tr.localPosition.x - trSize, 0, 0);
            }
        }

        private void SetSpeed(Movable movable, float position, float target, float delta) {
            var currentSpeed = movable.CurrentSpeed;
            var acceleration = movable.Acceleration;
            var closeRange = movable.LengthToStop;
            var range = target - position;
            float newSpeed;
            if (Math.Sign(range) != Math.Sign(currentSpeed)) {
                closeRange = 0;
            }
            if (Math.Abs(range) > closeRange) {
                newSpeed = currentSpeed + Math.Sign(range) * acceleration * delta;
            }
            else {
                newSpeed = currentSpeed - Math.Sign(range) * acceleration * delta;
                if (Math.Sign(newSpeed) != Math.Sign(currentSpeed) || Math.Sign(range) != Math.Sign(newSpeed))
                    newSpeed = 0;
            }

            movable.CurrentSpeed = BoundValue(newSpeed, 1);
        }

        private float BoundValue(float value, float bound) {
            return value > bound ? bound : value < -bound ? -bound : value;
        }

        public void Destroy() { }
    }
}