using System;
using System.Collections.Generic;
using System.Linq;
using Sonder.Scripts.Components.Entities;

namespace DefaultNamespace {
    public static class MakePath {
        public static void Do(Human human, Room room) {
            if (human.Movable.WorldPosition.Room == room) return;
            var path = GetMinimalPath(room, human.Movable.WorldPosition.Room);
            human.ActionQueue.Interrupt();
            for (var i = 0; i < path.Count; i++) {
                human.ActionQueue.AddWalk(path[i].Usable.Body.Tr.localPosition.x + path[i].Usable.Body.Size.x / 2);
                human.ActionQueue.AddUse(path[i].Usable);
            }
        }

        private static List<Door> GetMinimalPath(Room targetRoom, Room startRoom) {
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