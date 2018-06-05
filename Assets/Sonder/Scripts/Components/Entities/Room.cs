using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public float size;
    public List<Door> doors = new List<Door>();
    public float floor = 1;
    public Disabable disabable;
    public Transform tr;

    public static Room New(EcsSonderGameWorld _world, GameObject roomObject)
    {
        var roomEntity = _world.CreateEntity();
        var newRoom = _world.AddComponent<Room>(roomEntity);
        var disabable = _world.AddComponent<Disabable>(roomEntity);
        newRoom.size = roomObject.GetComponent<Collider2D>().bounds.size.x;
        newRoom.disabable = disabable;
        newRoom.disabable.sprites = roomObject.GetComponentsInChildren<SpriteRenderer>();
        newRoom.disabable.SetActive(false);
        newRoom.tr = roomObject.GetComponent<Transform>();
        registerDoors(_world, roomObject, newRoom);
        return newRoom;
    }

    private static void registerDoors(EcsSonderGameWorld _world, GameObject room, Room roomComponent)
    {
        DoorTag[] doorsStartRoom = room.GetComponentsInChildren<DoorTag>();
        foreach (DoorTag door in doorsStartRoom)
        {
            roomComponent.doors.Add(Door.New(_world, door.gameObject, roomComponent));
        }
    }
}