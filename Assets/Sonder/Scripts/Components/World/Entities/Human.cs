using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.Parts.Mind;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities {
    public class Human : BaseEntity {
        public bool InputControlled;
        public Disabable Disabable;
        public WorldPosition WorldPosition;
        public Storage Storage;
        public ActionQueue ActionQueue;
        public Movable Movable;
        public DrawableSprite renderer;

        public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
            var human = CreateThis<Human>(world);

            var body = human.AddComponent<Body>(world);
            human.WorldPosition = human.AddComponent<WorldPosition>(world);
            human.Movable = human.AddComponent<Movable>(world);
            human.Disabable = human.AddComponent<Disabable>(world);
            human.Storage = human.AddComponent<Storage>(world);
            human.ActionQueue = human.AddComponent<ActionQueue>(world);
            human.renderer = human.AddComponent<DrawableSprite>(world);

            body.init(humanObject);
            human.WorldPosition.init(body, startRoom);
            human.Movable.WorldPosition = human.WorldPosition;
            human.Disabable.init(humanObject);
            human.renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
            human.renderer.SetRandomColor();
            return human;
        }

        public void MakePlayer(EcsSonderGameWorld world) {
            world.AddComponent<InputControlled>(Entity);
            InputControlled = true;
            WorldPosition.Room.Disabable.SetActive(true);
            Disabable.SetActive(true);
        }
    }
}