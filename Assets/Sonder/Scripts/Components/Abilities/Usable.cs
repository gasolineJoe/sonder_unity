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
        public UsableEntity UsableEntity;

        public void Set(UsableEntity usableEntity, Type usableType, Body body) {
            Body = body;
            UsableType = usableType;
            UsableEntity = usableEntity;
            usableEntity.Usable = this;
        }
    }
}