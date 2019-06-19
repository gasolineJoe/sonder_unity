using System;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities.Usables {
    public class Door : UsableObject {
        public Door Destination;
        public Room Source;
        public Usable Usable;

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
            newDoor.Usable = world.AddComponent<Usable>(entity);
            var body = world.AddComponent<Body>(entity);
            body.init(doorObject);
            newDoor.Usable.Set(newDoor, Usable.Type.Door, body);
            newDoor.Source = sourceRoom;
            return newDoor;
        }
    }
}