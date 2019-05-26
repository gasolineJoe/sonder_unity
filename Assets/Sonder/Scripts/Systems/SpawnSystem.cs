using System.Collections.Generic;
using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class SpawnSystem : IEcsInitSystem {
    EcsSonderGameWorld _world = null;

    private GameObject _humanObject;
    private GameObject _startRoomObject;

    [Space] private GameObject[] _roomsObjects;

    public void Initialize() {
        this._humanObject = _world.StartupData.Hero;
        this._startRoomObject = _world.StartupData.startRoom;
        this._roomsObjects = _world.StartupData.rooms;

        Room newStartRoom = SpawnRoomWithPosition(_startRoomObject, 0, 0);
        int c = 0;
        List<Door> doors = new List<Door>();
        doors.AddRange(newStartRoom.Doors);
        while (c < doors.Count) {
            Room newRoom = SpawnRoomWithPosition(_roomsObjects[Random.Range(0, _roomsObjects.Length)], 20 * (c + 1), 0);
            if (newRoom.Doors.Count > 1) {
                doors.AddRange(newRoom.Doors.GetRange(1, newRoom.Doors.Count - 1));
            }

            doors[c].ConnectTo(newRoom.Doors[0]);
            c++;
        }

        for (int i = 0; i < 20; i++) {
            GameObject newHumanObject = Spawn(_humanObject);
            var human = Human.New(_world, newStartRoom, newHumanObject);
            human.TravelTo(newStartRoom);
            human.Tr.position = new Vector3(newHumanObject.transform.position.x, newStartRoom.Floor,
                newHumanObject.transform.position.z);

            if (i == 0) {
                GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().playerTransform =
                    human.Tr;
                human.MakePlayer(_world);
            }
        }
    }

    private Room SpawnRoomWithPosition(GameObject room, float x, float y) {
        GameObject roomObject = Spawn(room);
        var newRoom = Room.New(_world, roomObject);
        newRoom.Tr.position = new Vector3(x, y, 0);
        return newRoom;
    }

    private GameObject Spawn(GameObject gameObject) {
        return (GameObject) GameObject.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
    }

    public void Destroy() {
    }
}