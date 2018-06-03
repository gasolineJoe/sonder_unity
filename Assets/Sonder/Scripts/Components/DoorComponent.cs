using UnityEngine;
using UnityEditor;

public class DoorComponent
{
    public Transform tr;
    public int size;
    public DoorComponent destination;
    public RoomComponent source;

    public void ConnectTo(DoorComponent door)
    {
        if (destination == null && door.destination == null)
        {
            destination = door;
            door.destination = this;
        }
        else
        {
            throw (new System.Exception("This door is already connected to " + destination + "! Use DropConnection if you know what you are doing, smartass"));
        }
    }
}