using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Parts;
using Sonder.Scripts.Components.World.Items;
using UnityEngine;

namespace Sonder.Scripts.Components.World.Entities.Usables {
    public class Box : UsableEntity {
        public Storage Storage;

        public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
            var box = CreateThis<Box>(world);

            var body = box.AddComponent<Body>(world);
            box.Usable = box.AddComponent<Usable>(world);

            body.init(boxObject);
            box.Usable.Set(box, Usable.Type.Box, body);
            box.Storage = new Storage();
            box.Storage.name = "Box";

            box.Storage.Add(new Apple());
            box.Storage.Add(new Nail());
            box.Storage.Add(new Apple());
            return box;
        }
    }
}