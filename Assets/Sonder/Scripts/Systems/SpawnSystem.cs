﻿using System.Collections.Generic;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Entities;
using UnityEngine;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class SpawnSystem : IEcsInitSystem {
        EcsSonderGameWorld _world = null;

        private GameObject _humanObject;
        private GameObject _startRoomObject;

        [Space] private GameObject[] _roomsObjects;

        public void Initialize() {
            _humanObject = _world.StartupData.Hero;
            _startRoomObject = _world.StartupData.startRoom;
            _roomsObjects = _world.StartupData.rooms;

            var newStartRoom = SpawnRoomWithPosition(_startRoomObject, 0, 0);
            var c = 0;
            var doors = new List<Door>();
            doors.AddRange(newStartRoom.Doors);
            while (c < doors.Count) {
                var newRoom = SpawnRoomWithPosition(_roomsObjects[Random.Range(0, _roomsObjects.Length)], 20 * (c + 1), 0);
                if (newRoom.Doors.Count > 1) {
                    doors.AddRange(newRoom.Doors.GetRange(1, newRoom.Doors.Count - 1));
                }

                doors[c].ConnectTo(newRoom.Doors[0]);
                c++;
            }

            for (var i = 0; i < 20; i++) {
                var newHumanObject = Spawn(_humanObject);
                var human = Human.New(_world, newStartRoom, newHumanObject);
                human.TravelTo(newStartRoom);
                human.Body.Tr.position = new Vector3(newHumanObject.transform.position.x, newStartRoom.Floor,
                    newHumanObject.transform.position.z);

                if (i != 0) continue;
                GameObject.FindWithTag("MainCamera").GetComponent<CompleteCameraController>().playerTransform =
                    human.Body.Tr;
                human.MakePlayer(_world);
            }
        }

        private Room SpawnRoomWithPosition(GameObject room, float x, float y) {
            var roomObject = Spawn(room);
            var newRoom = Room.New(_world, roomObject);
            newRoom.Body.Tr.position = new Vector3(x, y, 0);
            return newRoom;
        }

        private GameObject Spawn(GameObject gameObject) {
            return Object.Instantiate(gameObject, GameObject.FindWithTag("GameWorld").transform);
        }

        public void Destroy() {
        }
    }
}