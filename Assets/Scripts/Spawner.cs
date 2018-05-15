using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    public GameObject hero;
    public GameObject startRoom;

    [Space]
    public GameObject[] rooms;

    public List<GameObject> spawnedRooms;

    private void Start()
    {
        GameObject newStartRoom = SpawnRoomWithPosition(startRoom, 0, 0);
        Door[] doorsStartRoom = newStartRoom.GetComponentsInChildren<Door>();
        newStartRoom.name = "start";

        for (int i = 0; i < doorsStartRoom.Length; i++)
        {
            GameObject newRoom = SpawnRoomWithPosition(rooms[Random.Range(0, rooms.Length)], 20 * (i + 1), 0);
            doorsStartRoom[i].ConnectTo(newRoom.GetComponent<Room>().data.doors[0]);
            newRoom.name = "room "+i;
            for (int j=1; j< newRoom.GetComponent<Room>().data.doors.Length; j++) {
                Destroy(newRoom.GetComponent<Room>().data.doors[j].gameObject);
            }
        }

        GameObject newHero = Spawn(hero);
        newHero.GetComponent<HeroMovement>().TravelTo(spawnedRooms[0].GetComponent<Room>());

        GameObject camera = GameObject.FindWithTag("MainCamera");
        CompleteCameraController cameraController = camera.GetComponent<CompleteCameraController>();
        cameraController.player = newHero;
    }

    private GameObject SpawnRoomWithPosition(GameObject room, float x, float y)
    {
        GameObject newRoom = Spawn(room);
        newRoom.GetComponent<Transform>().position = new Vector3(x, y, 0);
        newRoom.GetComponent<Room>().SetActive(false);
        spawnedRooms.Add(newRoom);
        return newRoom;
    }

    private GameObject Spawn(GameObject gameObject)
    {
        return (GameObject)Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
    }
}