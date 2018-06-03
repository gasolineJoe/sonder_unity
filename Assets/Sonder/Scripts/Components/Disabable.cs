using UnityEngine;
using UnityEditor;

public class Disabable
{
    public bool active;
    public SpriteRenderer[] sprites;

    void SetSubRenderersEnabled(bool isEnabled)
    {
        //= GetComponentsInChildren<SpriteRenderer>();
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