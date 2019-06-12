using System.Collections.Generic;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.UnityConnectStubs;
using UnityEngine;

namespace Sonder.Scripts.Components.Entities {
    public class Room {
        public Body Body;
        public readonly List<Door> Doors = new List<Door>();
        public readonly List<Box> Boxes = new List<Box>();
        public readonly List<Usable> Usables = new List<Usable>();
        public readonly float Floor = 1;
        public Disabable Disabable;
        public readonly List<Human> LocalHumans = new List<Human>();

        public static Room New(EcsSonderGameWorld world, GameObject roomObject) {
            var roomEntity = world.CreateEntity();
            var newRoom = world.AddComponent<Room>(roomEntity);
            var disabable = world.AddComponent<Disabable>(roomEntity);
            newRoom.Body = world.AddComponent<Body>(roomEntity);
            newRoom.Body.init(roomObject.GetComponent<Transform>(), roomObject.GetComponent<Collider2D>().bounds.size.x, 10);
            newRoom.Disabable = disabable;
            newRoom.Disabable.Sprites = roomObject.GetComponentsInChildren<SpriteRenderer>();
            newRoom.Disabable.SetActive(false);
            newRoom.RegisterDoors(world, roomObject);
            newRoom.RegisterBoxes(world, roomObject);
            return newRoom;
        }

        private void RegisterDoors(EcsSonderGameWorld world, GameObject room) {
            var doorsInRoom = room.GetComponentsInChildren<DoorTag>();
            foreach (var door in doorsInRoom) {
                var d = Door.New(world, door.gameObject, this);
                Doors.Add(d);
                Usables.Add(d.Usable);
            }
        }

        private void RegisterBoxes(EcsSonderGameWorld world, GameObject room) {
            var boxesInRoom = room.GetComponentsInChildren<BoxTag>();
            foreach (var box in boxesInRoom) {
                var b = Box.New(world, box.gameObject);
                Boxes.Add(b);
                Usables.Add(b.Usable);
            }
        }
    }
}