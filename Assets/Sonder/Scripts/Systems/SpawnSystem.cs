using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class SpawnSystem : IEcsInitSystem
{
    EcsSonderGameWorld _world = null;

    private GameObject humanObject;
    private GameObject startRoomObject;

    [Space]
    private GameObject[] roomsObjects;

    public void Initialize()
    {
        this.humanObject = _world.startupData.Hero;
        this.startRoomObject = _world.startupData.startRoom;
        this.roomsObjects = _world.startupData.rooms;

        Room newStartRoom = SpawnRoomWithPosition(startRoomObject, 0, 0);
        for (int i = 0; i < newStartRoom.doors.Count; i++)
        {
            Room newRoom = SpawnRoomWithPosition(roomsObjects[Random.Range(0, roomsObjects.Length)], 20 * (i + 1), 0);
            newStartRoom.doors[i].ConnectTo(newRoom.doors[0]);
        }

        for (int i = 0; i < 20; i++)
        {
            GameObject newHumanObject = Spawn(humanObject);
            var human = Human.New(_world, newStartRoom, newHumanObject);
            human.TravelTo(newStartRoom);
            human.tr.position = new Vector3(newHumanObject.transform.position.x, newStartRoom.floor, newHumanObject.transform.position.z);

            if (i == 0)
            {
                GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().playerTransform = human.tr;
                human.MakePlayer(_world);
            }
        }
    }

    private Room SpawnRoomWithPosition(GameObject room, float x, float y)
    {
        GameObject roomObject = Spawn(room);
        var newRoom = Room.New(_world, roomObject);
        newRoom.tr.position = new Vector3(x, y, 0);
        return newRoom;
    }

    private GameObject Spawn(GameObject gameObject)
    {
        return (GameObject)GameObject.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
    }

    public void Destroy()
    {
    }
}
