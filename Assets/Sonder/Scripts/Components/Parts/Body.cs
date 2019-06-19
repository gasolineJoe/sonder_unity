using System;
using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Body {
        public Transform Tr;
        public Vector2 Size;

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