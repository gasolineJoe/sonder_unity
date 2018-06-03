using UnityEngine;
using UnityEditor;

public class Human
{
    public Transform tr;
    public float size = 2;
    public RoomComponent currentRoom;

    public void travelTo(RoomComponent newRoom)
    {
        currentRoom.disabable.SetActive(false);
        tr.SetParent(newRoom.tr);
        newRoom.disabable.SetActive(true);
        currentRoom = newRoom;
    }
}