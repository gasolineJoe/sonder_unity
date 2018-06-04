using LeopotamGroup.Ecs;
using UnityEngine;

[EcsInject]
public class SpawnSystem : IEcsInitSystem
{
    EcsSonderGameWorld _world = null;

    private GameObject hero;
    private GameObject startRoom;

    [Space]
    private GameObject[] rooms;

    public void Initialize()
    {
        this.hero = _world.startupData.Hero;
        this.startRoom = _world.startupData.startRoom;
        this.rooms = _world.startupData.rooms;

        Room newStartRoom = SpawnRoomWithPosition(startRoom, 0, 0);
        for (int i = 0; i < newStartRoom.doors.Count; i++)
        {
            Room newRoom = SpawnRoomWithPosition(rooms[Random.Range(0, rooms.Length)], 20 * (i + 1), 0);
            newStartRoom.doors[i].ConnectTo(newRoom.doors[0]);
        }

        for (int i = 0; i < 200; i++)
        {
            GameObject newHero = Spawn(hero);
            var human = Human.New(_world, newStartRoom, newHero);
            human.TravelTo(newStartRoom);
            human.tr.position = new Vector3(newHero.transform.position.x, newStartRoom.floor, newHero.transform.position.z);

            GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().player = newHero;
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
