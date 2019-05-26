using UnityEngine;

public class Disabable {
    public bool Active = true;
    public SpriteRenderer[] Sprites;

    void SetSubRenderersEnabled(bool isEnabled) {
        for (int i = 0; i < Sprites.Length; i++) {
            Sprites[i].enabled = isEnabled;
        }
    }

    public void SetActive(bool isActive) {
        Active = isActive;
        SetSubRenderersEnabled(isActive);
    }
}