using UnityEngine;
using UnityEditor;

public class Human
{
    public int entity;
    public Transform tr;
    public float size = 2;
    public Room currentRoom;
    public bool inputControlled = false;
    public Disabable disabable;

    public static Human New(EcsSonderGameWorld _world, Room startRoom, GameObject humanObject)
    {
        var _entity = _world.CreateEntity();
        var human = _world.AddComponent<Human>(_entity);
        human.entity = _entity;
        human.tr = humanObject.transform;
        human.currentRoom = startRoom;
        _world.AddComponent<Movable>(_entity);
        _world.AddComponent<ObjectUser>(_entity);
        human.disabable = _world.AddComponent<Disabable>(_entity);
        human.disabable.sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
        DrawableSprite renderer = _world.AddComponent<DrawableSprite>(_entity);
        renderer.spriteRenderer = humanObject.GetComponent<SpriteRenderer>();
        renderer.setRandomColor();
        return human;
    }

    public Human MakePlayer(EcsSonderGameWorld _world)
    {
        _world.AddComponent<InputControlled>(entity);
        inputControlled = true;
        currentRoom.disabable.SetActive(true);
        disabable.SetActive(true);
        return this;
    }

    public void TravelTo(Room newRoom)
    {        
        tr.SetParent(newRoom.tr);
        var oldRoom = currentRoom;
        currentRoom = null;
        oldRoom.localHumans.Remove(this);

        if (inputControlled)
        {
            newRoom.disabable.SetActive(true);
            oldRoom.disabable.SetActive(false);
            foreach (Human h in oldRoom.localHumans)
            {
                h.disabable.SetActive(false);
            }
            foreach (Human h in newRoom.localHumans)
            {
                h.disabable.SetActive(true);
            }
        }

        newRoom.localHumans.Add(this);
        if (!inputControlled) disabable.SetActive(newRoom.disabable.active);
        currentRoom = newRoom;
    }
}