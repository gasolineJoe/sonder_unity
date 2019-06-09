using UnityEngine;

namespace Sonder.Scripts {
    public class CompleteCameraController : MonoBehaviour {
        public Transform playerTransform;

        void LateUpdate() {
            if (playerTransform != null)
                transform.position = playerTransform.position;
            transform.position = new Vector3(transform.position.x + 1, transform.position.y + 4, -10);
        }
    }
}