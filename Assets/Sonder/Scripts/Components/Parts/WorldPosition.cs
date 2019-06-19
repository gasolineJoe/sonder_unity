using Sonder.Scripts.Components.World.Entities;

namespace Sonder.Scripts.Components.Parts {
    public class WorldPosition {
        public Body Body;
        public Room Room;

        public void init(Body body, Room room) {
            Room = room;
            Body = body;
        }
    }
}