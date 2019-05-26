using UnityEngine;

public class Box : UsableObject {
    public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
        var entity = world.CreateEntity();
        var newBox = world.AddComponent<Box>(entity);
        world.AddComponent<Usable>(entity).Set(newBox, Usable.Type.Box, boxObject.transform, 3);
        return newBox;
    }
}