using UnityEngine;

namespace Sonder.Scripts.Systems {
    public class BaseDelayedSystem {
        protected float Delay = 0.25f;
        private float _nextUpdateTime;

        protected bool CantUpdate() {
            if (Time.time < _nextUpdateTime) {
                return true;
            }

            _nextUpdateTime = Time.time + Delay;
            return false;
        }
    }
}