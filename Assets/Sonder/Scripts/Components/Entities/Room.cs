using System.Collections.Generic;
using UnityEngine;

public class Room {
    public float Size;
    public readonly List<Door> Doors = new List<Door>();
    public readonly List<Box> Boxes = new List<Box>();
    public readonly float Floor = 1;
    public Disabable Disabable;
    public Transform Tr;
    public readonly List<Human> LocalHumans = new List<Human>();

    public static Room New(EcsSonderGameWorld world, GameObject roomObject) {
        var roomEntity = world.CreateEntity();
        var newRoom = world.AddComponent<Room>(roomEntity);
        var disabable = world.AddComponent<Disabable>(roomEntity);
        newRoom.Size = roomObject.GetComponent<Collider2D>().bounds.size.x;
        newRoom.Disabable = disabable;
        newRoom.Disabable.Sprites = roomObject.GetComponentsInChildren<SpriteRenderer>();
        newRoom.Disabable.SetActive(false);
        newRoom.Tr = roomObject.GetComponent<Transform>();
        newRoom.RegisterDoors(world, roomObject);
        newRoom.RegisterBoxes(world, roomObject);
        return newRoom;
    }

    private void RegisterDoors(EcsSonderGameWorld world, GameObject room) {
        var doorsInRoom = room.GetComponentsInChildren<DoorTag>();
        foreach (var door in doorsInRoom) {
            Doors.Add(Door.New(world, door.gameObject, this));
        }
    }

    private void RegisterBoxes(EcsSonderGameWorld world, GameObject room) {
        var boxesInRoom = room.GetComponentsInChildren<BoxTag>();
        foreach (var box in boxesInRoom) {
            Boxes.Add(Box.New(world, box.gameObject));
            Debug.Log("found box");
        }
    }
}