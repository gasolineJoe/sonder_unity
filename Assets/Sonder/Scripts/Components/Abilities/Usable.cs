using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Entities.Usables;
using UnityEngine;

namespace Sonder.Scripts.Components.Abilities {
    public class Usable {
        public enum Type {
            Door,
            Box
        }

        public Body Body;
        public Type UsableType;
        public UsableObject UsableObject;

        public void Set(UsableObject usableObject, Type usableType, Body body) {
            Body = body;
            UsableType = usableType;
            UsableObject = usableObject;
            usableObject.Usable = this;
        }
    }
}