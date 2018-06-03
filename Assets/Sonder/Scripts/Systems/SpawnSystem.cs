﻿using LeopotamGroup.Ecs;
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

        GameObject newStartRoom = SpawnRoomWithPosition(startRoom, 0, 0);
        Door[] doorsStartRoom = newStartRoom.GetComponentsInChildren<Door>();
        newStartRoom.name = "start";

        for (int i = 0; i < doorsStartRoom.Length; i++)
        {
            GameObject newRoom = SpawnRoomWithPosition(rooms[Random.Range(0, rooms.Length)], 20 * (i + 1), 0);
            doorsStartRoom[i].ConnectTo(newRoom.GetComponentsInChildren<Door>()[0]);
            newRoom.name = "room " + i;
            for (int j = 1; j < newRoom.GetComponentsInChildren<Door>().Length; j++)
            {
                Object.Destroy(newRoom.GetComponentsInChildren<Door>()[j].gameObject);
            }
        }

        GameObject newHero = Spawn(hero);
        newHero.GetComponent<RoomTraveller>().TravelTo(spawnedRooms[0].GetComponent<Room>());
        newHero.transform.position = new Vector3(newHero.transform.position.x, spawnedRooms[0].GetComponentInChildren<Floor>().GetFloor(), newHero.transform.position.z);
        var dude = _world.CreateEntity();
        var human = _world.AddComponent<Human>(dude);
        var movable = _world.AddComponent<Movable>(dude);
        _world.AddComponent<InputControlled>(dude);
        human.tr = newHero.transform;

        GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().player = newHero;
        GameObject.FindWithTag("GameLogic").GetComponent<InputHandler>().SetControlledPerson(newHero.GetComponent<Person>());
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
        return (GameObject)GameObject.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
    }

    public void Destroy()
    {
    }
}
