using System.Collections.Generic;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Items;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities.Usables {
    public class Box : UsableObject {
        public List<Item> Items;

        public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
            var entity = world.CreateEntity();
            var newBox = world.AddComponent<Box>(entity);
            var body = world.AddComponent<Body>(entity);
            List<Item> items = new List<Item>();
            items.Add(new Apple());
            items.Add(new Nail());
            items.Add(new Apple());
            newBox.Items = items;
            body.init(boxObject);
            world.AddComponent<Usable>(entity).Set(newBox, Usable.Type.Box, body);
            return newBox;
        }
    }
}