using UnityEngine;
using UnityEditor;

public class Human
{
    public Transform tr;
    public float size = 2;
    public Room currentRoom;

    public static Human New(EcsSonderGameWorld _world, Room startRoom, GameObject humanObject)
    {
        var humanEntity = _world.CreateEntity();
        var human = _world.AddComponent<Human>(humanEntity);
        human.tr = humanObject.transform;
        human.currentRoom = startRoom;
        _world.AddComponent<Movable>(humanEntity);
        _world.AddComponent<InputControlled>(humanEntity);
        _world.AddComponent<ObjectUser>(humanEntity);
        return human;
    }

    public void TravelTo(Room newRoom)
    {
        currentRoom.disabable.SetActive(false);
        tr.SetParent(newRoom.tr);
        newRoom.disabable.SetActive(true);
        currentRoom = newRoom;
    }
}