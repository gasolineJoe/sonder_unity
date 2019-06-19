using Sonder.Scripts.Components.Entities;

namespace Sonder.Scripts.Components.Abilities {
    public class WorldPosition {
        public Body Body;
        public Room Room;

        public void init(Body body, Room room) {
            Room = room;
            Body = body;
        }
    }
}