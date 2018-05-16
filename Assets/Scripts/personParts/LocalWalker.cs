using UnityEngine;
using System.Collections;

public class LocalWalker: MonoBehaviour
{
    public float speed = 0.1f;

    public void WalkInCurrentRoom(float xAxis)
    {
        float movementX = xAxis * speed;
        float position = transform.localPosition.x + movementX + GetComponent<Collider2D>().bounds.size.x;
        float roomSize = GetComponent<RoomTraveller>().currentRoom.GetComponent<Collider2D>().bounds.size.x;
        if ( position < roomSize && transform.localPosition.x + movementX > 0)
            transform.position = new Vector3(transform.position.x + movementX, transform.position.y, transform.position.z);
    }
}
