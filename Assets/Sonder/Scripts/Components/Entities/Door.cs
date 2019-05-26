using System;
using UnityEngine;

public class Door {
    public Transform Tr;
    public int Size;
    public Door Destination;
    public Room Source;

    public void ConnectTo(Door door) {
        if (Destination == null && door.Destination == null) {
            Destination = door;
            door.Destination = this;
        }
        else {
            throw new Exception("This door is already connected to " + Destination +
                                "! Use DropConnection if you know what you are doing, smartass");
        }
    }

    public static Door New(EcsSonderGameWorld world, GameObject doorObject, Room sourceRoom) {
        var newDoor = world.CreateEntityWith<Door>();
        newDoor.Tr = doorObject.transform;
        newDoor.Source = sourceRoom;
        newDoor.Size = 3;
        return newDoor;
    }
}