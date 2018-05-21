using UnityEngine;

public class RoomTraveller : MonoBehaviour {
    [SerializeField]
    public Room currentRoom = null;

    public void TravelTo(Room newRoom) {
        currentRoom?.SetActive(false);
        this.transform.SetParent(newRoom.transform);
        newRoom.SetActive(true);
        currentRoom = newRoom;
    }
}
