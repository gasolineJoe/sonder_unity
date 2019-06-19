using Sonder.Scripts.Components.Entities;

namespace DefaultNamespace {
    public static class TravelToRoom {
        public static void Do(Human human, Room newRoom) {
            human.WorldPosition.Body.Tr.SetParent(newRoom.Body.Tr);
            var oldRoom = human.Movable.WorldPosition.Room;
            human.Movable.WorldPosition.Room = null;
            oldRoom.LocalHumans.Remove(human);

            if (human.InputControlled) {
                newRoom.Disabable.SetActive(true);
                oldRoom.Disabable.SetActive(false);
                foreach (var h in oldRoom.LocalHumans) {
                    h.Disabable.SetActive(false);
                }

                foreach (var h in newRoom.LocalHumans) {
                    h.Disabable.SetActive(true);
                }
            }

            newRoom.LocalHumans.Add(human);
            if (!human.InputControlled) human.Disabable.SetActive(newRoom.Disabable.Active);
            human.Movable.WorldPosition.Room = newRoom;
        }
    }
}