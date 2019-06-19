using Sonder.Scripts.UnityConnectStubs;
using UnityEngine;

namespace Sonder.Scripts.Components.Parts {
    public class Body {
        public Transform Tr;
        public Vector2 Size;

        public void init(GameObject gameObject) {
            var dimen = gameObject.GetComponent<Dimensions>();
            if (dimen != null) {
                init(gameObject.transform, dimen.width, dimen.height);
            }
            else {
                Debug.LogError("no dimensions for " + gameObject + " you dummy!");
            }
        }

        public void init(Transform tr, float width, float height) {
            Tr = tr;
            Size = new Vector2(width, height);
        }

        public bool isInBounds(Vector2 point) {
            var localPosition = Tr.localPosition;
            return point.x > localPosition.x &&
                   Size.x > point.x - localPosition.x &&
                   point.y > localPosition.y &&
                   Size.y > point.y - localPosition.y;
        }
    }
}