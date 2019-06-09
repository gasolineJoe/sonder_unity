using Sonder.Scripts.Components.Entities;

namespace Sonder.Scripts.Components.Abilities {
    using UnityEngine;

    public class Usable {
        public Transform Tr;

        public enum Type {
            Door,
            Box
        }

        public int Size;
        public Type UsableType;
        public UsableObject UsableObject;

        public void Set(UsableObject usableObject, Type usableType, Transform tr, int size) {
            Tr = tr;
            Size = size;
            UsableType = usableType;
            UsableObject = usableObject;
            usableObject.Usable = this;
        }
    }
}