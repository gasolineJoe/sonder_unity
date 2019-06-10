using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using UnityEngine;

namespace Sonder.Scripts.Components.Entities {
    public class Human {
        private int _entity;
        private bool _inputControlled;
        public Disabable Disabable;
        public Body Body;
        public Storage Storage;
        public ActionQueue ActionQueue;
        public Movable Movable;

        public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
            var entity = world.CreateEntity();
            var human = world.AddComponent<Human>(entity);
            human._entity = entity;
            human.Body = world.AddComponent<Body>(entity);
            human.Body.init(humanObject.transform, 2);
            human.Movable = world.AddComponent<Movable>(entity);
            human.Movable.Body = human.Body;
            human.Movable.CurrentRoom = startRoom;
            world.AddComponent<ObjectUser>(entity);
            human.Disabable = world.AddComponent<Disabable>(entity);
            human.Storage = world.AddComponent<Storage>(entity);
            human.ActionQueue = world.AddComponent<ActionQueue>(entity);
            human.Disabable.Sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
            var renderer = world.AddComponent<DrawableSprite>(entity);
            renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
            renderer.SetRandomColor();
            return human;
        }

        public Human MakePlayer(EcsSonderGameWorld world) {
            world.AddComponent<InputControlled>(_entity);
            _inputControlled = true;
            Movable.CurrentRoom.Disabable.SetActive(true);
            Disabable.SetActive(true);
            return this;
        }

        public void TravelTo(Room newRoom) {
            Body.Tr.SetParent(newRoom.Body.Tr);
            var oldRoom = Movable.CurrentRoom;
            Movable.CurrentRoom = null;
            oldRoom.LocalHumans.Remove(this);

            if (_inputControlled) {
                newRoom.Disabable.SetActive(true);
                oldRoom.Disabable.SetActive(false);
                foreach (var h in oldRoom.LocalHumans) {
                    h.Disabable.SetActive(false);
                }

                foreach (var h in newRoom.LocalHumans) {
                    h.Disabable.SetActive(true);
                }
            }

            newRoom.LocalHumans.Add(this);
            if (!_inputControlled) Disabable.SetActive(newRoom.Disabable.Active);
            Movable.CurrentRoom = newRoom;
        }
    }
}