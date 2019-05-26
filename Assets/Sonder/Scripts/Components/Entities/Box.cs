using UnityEngine;

public class Box {
    public Transform Tr;
    public int Size;
    
    public static Box New(EcsSonderGameWorld world, GameObject boxObject) {
        var newBox = world.CreateEntityWith<Box>();
        newBox.Tr = boxObject.transform;
        newBox.Size = 3;
        return newBox;
    }
}
