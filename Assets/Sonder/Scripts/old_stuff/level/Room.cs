﻿using UnityEngine;

public class Room : MonoBehaviour
{
    public bool active;

    void SetSubRenderersEnabled(bool isEnabled)
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
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