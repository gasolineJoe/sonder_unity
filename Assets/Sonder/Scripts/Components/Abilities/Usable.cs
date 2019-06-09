using Sonder.Scripts.Components.Entities;
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

        public void Set(UsableObject usableObject, Type usableType, Transform tr, int size) {
            Body = new Body();
            Body.init(tr, size);
            UsableType = usableType;
            UsableObject = usableObject;
            usableObject.Usable = this;
        }
    }
}