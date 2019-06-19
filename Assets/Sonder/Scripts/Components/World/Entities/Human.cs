using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.Parts.Mind;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities {
    public class Human {
        private int _entity;
        public bool InputControlled;
        public Disabable Disabable;
        public WorldPosition WorldPosition;
        public Storage Storage;
        public ActionQueue ActionQueue;
        public Movable Movable;

        public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
            var entity = world.CreateEntity();
            var human = world.AddComponent<Human>(entity);
            human._entity = entity;
            var body = world.AddComponent<Body>(entity);
            body.init(humanObject);
            human.WorldPosition = world.AddComponent<WorldPosition>(entity);
            human.WorldPosition.init(body, startRoom);
            human.Movable = world.AddComponent<Movable>(entity);
            human.Movable.WorldPosition = human.WorldPosition;
            human.Disabable = world.AddComponent<Disabable>(entity);
            human.Storage = world.AddComponent<Storage>(entity);
            human.ActionQueue = world.AddComponent<ActionQueue>(entity);
            human.Disabable.Sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
            var renderer = world.AddComponent<DrawableSprite>(entity);
            renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
            renderer.SetRandomColor();
            return human;
        }

        public void MakePlayer(EcsSonderGameWorld world) {
            world.AddComponent<InputControlled>(_entity);
            InputControlled = true;
            WorldPosition.Room.Disabable.SetActive(true);
            Disabable.SetActive(true);
        }
    }
}