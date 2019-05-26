using UnityEngine;

public class Human {
    public int Entity;
    public Transform Tr;
    public readonly float Size = 2;
    public Room CurrentRoom;
    public bool InputControlled;
    public Disabable Disabable;

    public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
        var entity = world.CreateEntity();
        var human = world.AddComponent<Human>(entity);
        human.Entity = entity;
        human.Tr = humanObject.transform;
        human.CurrentRoom = startRoom;
        world.AddComponent<Movable>(entity);
        world.AddComponent<ObjectUser>(entity);
        human.Disabable = world.AddComponent<Disabable>(entity);
        human.Disabable.Sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
        DrawableSprite renderer = world.AddComponent<DrawableSprite>(entity);
        renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
        renderer.SetRandomColor();
        return human;
    }

    public Human MakePlayer(EcsSonderGameWorld world) {
        world.AddComponent<InputControlled>(Entity);
        InputControlled = true;
        CurrentRoom.Disabable.SetActive(true);
        Disabable.SetActive(true);
        return this;
    }

    public void TravelTo(Room newRoom) {
        Tr.SetParent(newRoom.Tr);
        var oldRoom = CurrentRoom;
        CurrentRoom = null;
        oldRoom.LocalHumans.Remove(this);

        if (InputControlled) {
            newRoom.Disabable.SetActive(true);
            oldRoom.Disabable.SetActive(false);
            foreach (Human h in oldRoom.LocalHumans) {
                h.Disabable.SetActive(false);
            }

            foreach (Human h in newRoom.LocalHumans) {
                h.Disabable.SetActive(true);
            }
        }

        newRoom.LocalHumans.Add(this);
        if (!InputControlled) Disabable.SetActive(newRoom.Disabable.Active);
        CurrentRoom = newRoom;
    }
}