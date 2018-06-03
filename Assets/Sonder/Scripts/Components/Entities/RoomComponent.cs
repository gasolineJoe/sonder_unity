using System.Collections.Generic;
using UnityEngine;

public class RoomComponent
{
    public float size;
    public List<DoorComponent> doors = new List<DoorComponent>();
    public float floor = 1;
    public Disabable disabable;
    public Transform tr;
}