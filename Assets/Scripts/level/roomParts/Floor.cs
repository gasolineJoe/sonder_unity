using UnityEngine;

public class Floor : MonoBehaviour {
    public float GetFloor()
    {
        return GetComponent<Collider2D>().bounds.size.y + transform.position.y;
    }
}
