using Sonder.Scripts.Components.Entities;

namespace Sonder.Scripts.Components.Abilities {
    public class Movable {
        public float Speed = 10f;
        public float Acceleration = 0;
        public Body Body;
        public Room CurrentRoom;
    }
}