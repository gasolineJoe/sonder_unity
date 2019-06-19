using System;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts.Mind;
using Sonder.Scripts.Utils;
using UnityEngine;

namespace Sonder.Scripts.Actions.ActionExecutors {
    public static class Move {
        public static void Do(Movable movable, ActionQueue actionQueue, float delay) {
            var tr = movable.WorldPosition.Body.Tr;
            var to = FixTarget((float) actionQueue.GetAction().Item2, movable);
            var currentPosition = tr.localPosition.x + movable.WorldPosition.Body.Size.x / 2;
            var xDelta = movable.CurrentSpeed * movable.MaxSpeed * delay;
            SetLengthToStop(currentPosition, movable, to);
            SetSpeed(movable, currentPosition, to, delay);
            TranslateWithBounds(movable, tr, xDelta, movable.WorldPosition.Body.Size.x,
                movable.WorldPosition.Room.Body.Size.x);
            CheckDone(movable, actionQueue);
        }

        private static float FixTarget(float target, Movable movable) {
            if (target < 0) return 0;
            if (target > movable.WorldPosition.Room.Body.Size.x) return movable.WorldPosition.Room.Body.Size.x - 1;
            return target;
        }

        private static void CheckDone(Movable movable, ActionQueue queue) {
            if (!(Math.Abs(movable.CurrentSpeed) < 0.01f)) return;
            queue.ActionDone();
            movable.LengthToStop = 0;
        }

        private static void SetLengthToStop(float position, Movable movable, float target) {
            if (!(Math.Abs(movable.LengthToStop) < 0.01f)) return;
            var timeToStop = 1 / movable.Acceleration;
            movable.LengthToStop = movable.MaxSpeed / 1.82f * timeToStop;
            if (movable.LengthToStop * 2 > Math.Abs(target - position))
                movable.LengthToStop = Math.Abs(target - position) / 2;
        }

        private static void TranslateWithBounds(Movable movable, Transform tr, float xDelta, float trSize,
            float rightBound) {
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

        private static void SetSpeed(Movable movable, float position, float target, float delta) {
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

            movable.CurrentSpeed = S.BoundValue(newSpeed, 1);
        }
    }
}