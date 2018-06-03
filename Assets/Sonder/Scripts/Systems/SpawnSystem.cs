using LeopotamGroup.Ecs;
using System.Collections.Generic;
using UnityEngine;

[EcsInject]
public class SpawnSystem : IEcsInitSystem
{
    EcsSonderGameWorld _world = null;

    private GameObject hero;
    private GameObject startRoom;

    [Space]
    private GameObject[] rooms;

    private List<GameObject> spawnedRooms = new List<GameObject>();

    public void Initialize()
    {
        this.hero = _world.startupData.Hero;
        this.startRoom = _world.startupData.startRoom;
        this.rooms = _world.startupData.rooms;

        RoomComponent newStartRoom = SpawnRoomWithPosition(startRoom, 0, 0);

        for (int i = 0; i < newStartRoom.doors.Count; i++)
        {
            RoomComponent newRoom = SpawnRoomWithPosition(rooms[Random.Range(0, rooms.Length)], 20 * (i + 1), 0);
            newStartRoom.doors[i].ConnectTo(newRoom.doors[0]);
        }

        GameObject newHero = Spawn(hero);

        var dude = _world.CreateEntity();
        var human = _world.AddComponent<Human>(dude);
        human.tr = newHero.transform;
        human.currentRoom = newStartRoom;
        var movable = _world.AddComponent<Movable>(dude);
        _world.AddComponent<InputControlled>(dude);
        _world.AddComponent<ObjectUser>(dude);

        human.travelTo(newStartRoom);
        human.tr.position = new Vector3(newHero.transform.position.x, newStartRoom.floor, newHero.transform.position.z);

        GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().player = newHero;
    }

    private RoomComponent SpawnRoomWithPosition(GameObject room, float x, float y)
    {
        GameObject newRoom = Spawn(room);
        spawnedRooms.Add(newRoom);

        var roomEntity = _world.CreateEntity();
        var roomParameters = _world.AddComponent<RoomComponent>(roomEntity);
        var disabable = _world.AddComponent<Disabable>(roomEntity);
        roomParameters.size = newRoom.GetComponent<Collider2D>().bounds.size.x;
        roomParameters.disabable = disabable;
        roomParameters.disabable.sprites = newRoom.GetComponentsInChildren<SpriteRenderer>();
        roomParameters.tr = newRoom.GetComponent<Transform>();
        roomParameters.tr.position = new Vector3(x, y, 0);
        registerDoors(newRoom, roomParameters);

        roomParameters.disabable.SetActive(false);

        return roomParameters;
    }

    private GameObject Spawn(GameObject gameObject)
    {
        return (GameObject)GameObject.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
    }

    private void registerDoors(GameObject room, RoomComponent roomComponent)
    {
        Door[] doorsStartRoom = room.GetComponentsInChildren<Door>();
        foreach (Door door in doorsStartRoom)
        {
            var drcomponent = _world.CreateEntityWith<DoorComponent>();
            drcomponent.tr = door.gameObject.transform;
            drcomponent.source = roomComponent;
            drcomponent.size = 3;
            roomComponent.doors.Add(drcomponent);
        }
    }

    public void Destroy()
    {
    }
}
