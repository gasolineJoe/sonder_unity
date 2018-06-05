using UnityEngine;
using UnityEditor;

public class Disabable
{
    public bool active = true;
    public SpriteRenderer[] sprites;

    void SetSubRenderersEnabled(bool isEnabled)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = isEnabled;
        }
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
        SetSubRenderersEnabled(isActive);
    }
}