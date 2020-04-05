using System;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities.Usables {
    public class Door : UsableEntity {
        public Door Destination;
        public Room Source;

        public void ConnectTo(Door door) {
            if (Destination == null && door.Destination == null) {
                Destination = door;
                door.Destination = this;
            }
            else {
                throw new Exception("This door is already connected to " + Destination +
                                    "! Use DropConnection if you know what you are doing, smartass");
            }
        }

        public static Door New(EcsSonderGameWorld world, GameObject doorObject, Room sourceRoom) {
            var door = CreateThis<Door>(world);
            
            door.Usable = door.AddComponent<Usable>(world);
            var body = door.AddComponent<Body>(world);
            
            body.init(doorObject);
            door.Usable.Set(door, Usable.Type.Door, body);
            door.Source = sourceRoom;
            return door;
        }
    }
}