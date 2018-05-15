using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    public float speed = 5f;
    [SerializeField]
    List<GameObject> collisions = new List<GameObject>();
    public Room currentRoom = null;

    private void Update()
    {
        float movementX = Input.GetAxis("Horizontal") * speed;
        if (transform.localPosition.x + movementX + GetComponent<Collider2D>().bounds.size.x < currentRoom.GetComponent<Collider2D>().bounds.size.x && transform.localPosition.x + movementX > 0)
            transform.position = new Vector3(transform.position.x + movementX, transform.position.y, transform.position.z);


        foreach (GameObject something in collisions)
        {
            if (something.CompareTag("Door"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Vector3 newPos = something.GetComponent<Door>().destination.transform.position;
                    transform.position = newPos;

                    Room newRoom = something.GetComponent<Door>().destination.GetComponentInParent<Room>();
                    TravelTo(newRoom);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision.gameObject);
    }

    public void TravelTo(Room newRoom) {
        currentRoom?.SetActive(false);
        this.transform.SetParent(newRoom.transform);
        newRoom.SetActive(true);
        currentRoom = newRoom;
    }
}
