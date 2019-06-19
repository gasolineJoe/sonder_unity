using UnityEngine;

namespace Sonder.Scripts {
    public class CompleteCameraController : MonoBehaviour {
        public Transform playerTransform;
        private bool _isPlayerTransformNotNull;

        private void Start() {
            _isPlayerTransformNotNull = playerTransform != null;
        }

        private void LateUpdate() {
            if (!_isPlayerTransformNotNull) return;
            var pos = playerTransform.position;
            transform.position = new Vector3(pos.x + 1, pos.y + 4, -10);
        }
    }
}