using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Body {
        public Transform Tr;
        public float Size;

        public void init(Transform tr, float size) {
            Tr = tr;
            Size = size;
        }
    }
}