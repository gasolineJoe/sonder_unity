using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Items;

namespace Sonder.Scripts.Components.Entities {
    using System.Collections.Generic;
    using UnityEngine;

    public class Box : UsableObject {
        public List<Item> Items;

        public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
            var entity = world.CreateEntity();
            var newBox = world.AddComponent<Box>(entity);
            List<Item> items = new List<Item>();
            items.Add(new Apple());
            items.Add(new Nail());
            items.Add(new Apple());
            newBox.Items = items;
            world.AddComponent<Usable>(entity).Set(newBox, Usable.Type.Box, boxObject.transform, 3);
            return newBox;
        }
    }
}