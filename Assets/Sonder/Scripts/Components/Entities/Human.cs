using UnityEngine;
using UnityEditor;

public class Human
{
    public int entity;
    public Transform tr;
    public float size = 2;
    public Room currentRoom;

    public static Human New(EcsSonderGameWorld _world, Room startRoom, GameObject humanObject)
    {
        var _entity = _world.CreateEntity();
        var human = _world.AddComponent<Human>(_entity);
        human.entity = _entity;
        human.tr = humanObject.transform;
        human.currentRoom = startRoom;
        _world.AddComponent<Movable>(_entity);
        _world.AddComponent<ObjectUser>(_entity);
        return human;
    }

    public Human MakePlayer(EcsSonderGameWorld _world)
    {
        _world.AddComponent<InputControlled>(entity);
        return this;
    }

    public void TravelTo(Room newRoom)
    {
        currentRoom.disabable.SetActive(false);
        tr.SetParent(newRoom.tr);
        newRoom.disabable.SetActive(true);
        currentRoom = newRoom;
    }
}