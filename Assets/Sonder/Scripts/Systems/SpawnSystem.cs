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
        newHero.GetComponent<RoomTraveller>().TravelTo(spawnedRooms[0].GetComponent<Room>());
        newHero.transform.position = new Vector3(newHero.transform.position.x, spawnedRooms[0].GetComponentInChildren<Floor>().GetFloor(), newHero.transform.position.z);

        var dude = _world.CreateEntity();
        var human = _world.AddComponent<Human>(dude);
        human.tr = newHero.transform;
        human.currentRoom = newStartRoom;
        var movable = _world.AddComponent<Movable>(dude);
        _world.AddComponent<InputControlled>(dude);
        _world.AddComponent<ObjectUser>(dude);


        GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().player = newHero;
    }

    private RoomComponent SpawnRoomWithPosition(GameObject room, float x, float y)
    {
        GameObject newRoom = Spawn(room);
        newRoom.GetComponent<Transform>().position = new Vector3(x, y, 0);
        newRoom.GetComponent<Room>().SetActive(false);
        spawnedRooms.Add(newRoom);

        var roomParameters = _world.CreateEntityWith<RoomComponent>();
        roomParameters.size = newRoom.GetComponent<Collider2D>().bounds.size.x;
        registerDoors(newRoom, roomParameters);
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
