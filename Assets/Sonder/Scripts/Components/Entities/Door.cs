using System;
using Sonder.Scripts.Components.Abilities;
using UnityEngine;

namespace Sonder.Scripts.Components.Entities {
    public class Door : UsableObject {
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
            var entity = world.CreateEntity();
            var newDoor = world.AddComponent<Door>(entity);
            world.AddComponent<Usable>(entity).Set(newDoor, Usable.Type.Door, doorObject.transform, 2.5f, 4);
            newDoor.Source = sourceRoom;
            return newDoor;
        }
    }
}