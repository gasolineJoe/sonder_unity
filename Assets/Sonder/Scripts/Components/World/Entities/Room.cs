using System.Collections.Generic;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities.Usables;
using Sonder.Scripts.UnityConnectStubs;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities {
    public class Room : BaseEntity {
        public Body Body;
        public readonly List<Door> Doors = new List<Door>();
        public readonly List<Box> Boxes = new List<Box>();
        public readonly List<Usable> Usables = new List<Usable>();
        public readonly float Floor = 1;
        public Disabable Disabable;
        public readonly List<Human> LocalHumans = new List<Human>();

        public static Room New(EcsSonderGameWorld world, GameObject roomObject) {
            var room = CreateThis<Room>(world);

            room.Disabable = room.AddComponent<Disabable>(world);
            room.Body = room.AddComponent<Body>(world);

            room.Body.init(roomObject);
            room.Disabable.init(roomObject);
            room.Disabable.SetActive(false);
            room.RegisterDoors(world, roomObject);
            room.RegisterBoxes(world, roomObject);
            room.RegisterSigns(world, roomObject);
            return room;
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

        private void RegisterSigns(EcsSonderGameWorld world, GameObject room) {
            var signsInRoom = room.GetComponentsInChildren<SignTag>();
            foreach (var sign in signsInRoom) {
                var s = Sign.New(world, sign.gameObject);
                s.SetText(room.name);
            }
        }
    }
}