using System.Collections.Generic;
using UnityEngine;

public class Room {
    public float Size;
    public readonly List<Door> Doors = new List<Door>();
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
        RegisterDoors(world, roomObject, newRoom);
        return newRoom;
    }

    private static void RegisterDoors(EcsSonderGameWorld world, GameObject room, Room roomComponent) {
        DoorTag[] doorsStartRoom = room.GetComponentsInChildren<DoorTag>();
        foreach (DoorTag door in doorsStartRoom) {
            roomComponent.Doors.Add(Door.New(world, door.gameObject, roomComponent));
        }
    }
}