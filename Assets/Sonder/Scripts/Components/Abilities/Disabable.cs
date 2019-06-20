using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Disabable {
        public bool Active = true;
        public SpriteRenderer[] Sprites;
        public MeshRenderer[] MeshRenderers;

        void SetSubRenderersEnabled(bool isEnabled) {
            foreach (var sprite in Sprites) {
                sprite.enabled = isEnabled;
            }

            foreach (var meshRenderer in MeshRenderers) {
                meshRenderer.enabled = isEnabled;
            }
        }

        public void SetActive(bool isActive) {
            Active = isActive;
            SetSubRenderersEnabled(isActive);
        }

        public void init(GameObject gameObject) {
            Sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
            MeshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        }
    }
}