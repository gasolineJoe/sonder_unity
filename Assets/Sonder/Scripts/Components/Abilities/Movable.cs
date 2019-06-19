﻿using Sonder.Scripts.Components.Parts;

namespace Sonder.Scripts.Components.Abilities {
    public class Movable {
        public float MaxSpeed = 10f;
        public float CurrentSpeed = 0;
        public float Acceleration = 2f;
        public float LengthToStop = 0;
        public WorldPosition WorldPosition;
    }
}