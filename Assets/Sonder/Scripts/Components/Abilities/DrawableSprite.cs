using UnityEngine;
using UnityEditor;

public class DrawableSprite
{
    public SpriteRenderer spriteRenderer;

    public void setRandomColor()
    {
        spriteRenderer.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
    }
}