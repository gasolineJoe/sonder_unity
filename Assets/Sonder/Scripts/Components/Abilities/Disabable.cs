using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Disabable {
        public bool Active = true;
        public SpriteRenderer[] Sprites;

        void SetSubRenderersEnabled(bool isEnabled) {
            foreach (var sprite in Sprites) {
                sprite.enabled = isEnabled;
            }
        }

        public void SetActive(bool isActive) {
            Active = isActive;
            SetSubRenderersEnabled(isActive);
        }
    }
}