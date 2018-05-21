using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomData data;

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
        data.active = isActive;
        SetSubRenderersEnabled(isActive);
    }
}

[System.Serializable]
public struct RoomData
{
    public bool active;
}