using UnityEngine;
using UnityEditor;

public class Door
{
    public Transform tr;
    public int size;
    public Door destination;
    public Room source;

    public void ConnectTo(Door door)
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

    public static Door New(EcsSonderGameWorld _world, GameObject doorObject, Room sourceRoom)
    {
        var newDoor = _world.CreateEntityWith<Door>();
        newDoor.tr = doorObject.transform;
        newDoor.source = sourceRoom;
        newDoor.size = 3;
        return newDoor;
    }
}