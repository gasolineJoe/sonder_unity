using System.Collections.Generic;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Items;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities.Usables {
    public class Box : UsableEntity {
        public List<Item> Items;

        public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
            var box = CreateThis<Box>(world);

            var body = box.AddComponent<Body>(world);
            box.Usable = box.AddComponent<Usable>(world);
            
            body.init(boxObject);
            box.Usable.Set(box, Usable.Type.Box, body);
            List<Item> items = new List<Item>();
            items.Add(new Apple());
            items.Add(new Nail());
            items.Add(new Apple());
            box.Items = items;
            return box;
        }
    }
}