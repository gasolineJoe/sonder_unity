using System;
using System.Collections.Generic;
using System.Linq;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using UnityEngine;

namespace Sonder.Scripts.Components.Entities {
    public class Human {
        private int _entity;
        private bool _inputControlled;
        public Disabable Disabable;
        public Body Body;
        public Storage Storage;
        public ActionQueue ActionQueue;
        public Movable Movable;

        public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
            var entity = world.CreateEntity();
            var human = world.AddComponent<Human>(entity);
            human._entity = entity;
            human.Body = world.AddComponent<Body>(entity);
            human.Body.init(humanObject.transform, 2, 3);
            human.Movable = world.AddComponent<Movable>(entity);
            human.Movable.Body = human.Body;
            human.Movable.CurrentRoom = startRoom;
            human.Disabable = world.AddComponent<Disabable>(entity);
            human.Storage = world.AddComponent<Storage>(entity);
            human.ActionQueue = world.AddComponent<ActionQueue>(entity);
            human.Disabable.Sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
            var renderer = world.AddComponent<DrawableSprite>(entity);
            renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
            renderer.SetRandomColor();
            return human;
        }

        public Human MakePlayer(EcsSonderGameWorld world) {
            world.AddComponent<InputControlled>(_entity);
            _inputControlled = true;
            Movable.CurrentRoom.Disabable.SetActive(true);
            Disabable.SetActive(true);
            return this;
        }

        public void TravelTo(Room newRoom) {
            Body.Tr.SetParent(newRoom.Body.Tr);
            var oldRoom = Movable.CurrentRoom;
            Movable.CurrentRoom = null;
            oldRoom.LocalHumans.Remove(this);

            if (_inputControlled) {
                newRoom.Disabable.SetActive(true);
                oldRoom.Disabable.SetActive(false);
                foreach (var h in oldRoom.LocalHumans) {
                    h.Disabable.SetActive(false);
                }

                foreach (var h in newRoom.LocalHumans) {
                    h.Disabable.SetActive(true);
                }
            }

            newRoom.LocalHumans.Add(this);
            if (!_inputControlled) Disabable.SetActive(newRoom.Disabable.Active);
            Movable.CurrentRoom = newRoom;
        }

        public void WalkTo(Room room) {
            if (Movable.CurrentRoom == room) return;
            var path = getMinimalPath(room, Movable.CurrentRoom);
            ActionQueue.Interrupt();
            for (int i = 0; i < path.Count; i++) {
                ActionQueue.AddWalk(path[i].Usable.Body.Tr.localPosition.x + path[i].Usable.Body.Size.x / 2);
                ActionQueue.AddUse(path[i].Usable);
            }
        }

        private static List<Door> getMinimalPath(Room targetRoom, Room startRoom) {
            var currentDoors = new List<Tuple<List<Door>, int>>();
            var startingPath = new List<Door>();
            var searchedRooms = new List<Room>();
            var startDoor = new Door {Destination = new Door {Source = startRoom}};
            startingPath.Add(startDoor);
            currentDoors.Add(new Tuple<List<Door>, int>(startingPath, int.MaxValue));

            while (currentDoors.Count > 0) {
                var nextDoors = new List<Tuple<List<Door>, int>>();
                for (var i = 0; i < currentDoors.Count; i++) {
                    var currentDoor = currentDoors[i].Item1.Last();
                    var currentRoom = currentDoor.Destination.Source;
                    if (targetRoom == currentRoom) {
                        var result = currentDoors[i].Item1;
                        result.Remove(result.First());
                        return result;
                    }

                    for (var d = 0; d < currentRoom.Doors.Count; d++) {
                        if (searchedRooms.Contains(currentRoom.Doors[d].Destination.Source)) continue;
                        var currentList = new List<Door>(currentDoors[i].Item1) {currentRoom.Doors[d]};
                        nextDoors.Add(new Tuple<List<Door>, int>(currentList, currentDoors[i].Item2 + 20));
                    }

                    searchedRooms.Add(currentRoom);
                }

                currentDoors = nextDoors;
            }

            return new List<Door>();
        }
    }
}