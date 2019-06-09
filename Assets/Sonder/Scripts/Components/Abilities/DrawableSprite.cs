using UnityEngine;

public class DrawableSprite {
    public SpriteRenderer SpriteRenderer;

    public void SetRandomColor() {
        SpriteRenderer.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
    }
}