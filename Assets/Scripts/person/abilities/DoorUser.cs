using UnityEngine;
using System.Collections.Generic;

public class DoorUser: MonoBehaviour
{
    [SerializeField]
    List<GameObject> collisions = new List<GameObject>();

    public void OnUse()
    {
        foreach (GameObject something in collisions)
        {
            Usable usable = something.GetComponent<Usable>();
            if (usable.Identify().Equals(Usable.UsableType.Door))
            {
                RoomTraveller traveller = GetComponent<RoomTraveller>();
                if (traveller != null)
                {
                    Vector3 newPos = something.GetComponent<Door>().destination.transform.position;
                    newPos = new Vector3(newPos.x, traveller.currentRoom.GetComponentInChildren<Floor>().GetFloor(), newPos.z);
                    transform.position = newPos;

                    Room newRoom = something.GetComponent<Door>().destination.GetComponentInParent<Room>();
                    traveller.TravelTo(newRoom);
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
}
