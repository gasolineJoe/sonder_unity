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
                Vector3 newPos = something.GetComponent<Door>().destination.transform.position;
                transform.position = newPos;

                Room newRoom = something.GetComponent<Door>().destination.GetComponentInParent<Room>();
                GetComponent<RoomTraveller>()?.TravelTo(newRoom);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponent<Usable>() != null) {
            collisions.Add(collision.gameObject);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision.gameObject);
    }
}
