using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Body {
        public Transform Tr;
        public Vector2 Size;

        public void init(Transform tr, float width, float height) {
            Tr = tr;
            Size = new Vector2(width, height);
        }
    }
}