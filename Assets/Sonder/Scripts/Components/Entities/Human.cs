using UnityEngine;

public class Human {
    private int _entity;
    public Transform Tr;
    public readonly float Size = 2;
    public Room CurrentRoom;
    private bool _inputControlled;
    private Disabable _disabable;
    public Storage Storage;

    public static Human New(EcsSonderGameWorld world, Room startRoom, GameObject humanObject) {
        var entity = world.CreateEntity();
        var human = world.AddComponent<Human>(entity);
        human._entity = entity;
        human.Tr = humanObject.transform;
        human.CurrentRoom = startRoom;
        world.AddComponent<Movable>(entity);
        world.AddComponent<ObjectUser>(entity);
        human._disabable = world.AddComponent<Disabable>(entity);
        human.Storage = world.AddComponent<Storage>(entity);
        human._disabable.Sprites = humanObject.GetComponentsInChildren<SpriteRenderer>();
        var renderer = world.AddComponent<DrawableSprite>(entity);
        renderer.SpriteRenderer = humanObject.GetComponent<SpriteRenderer>();
        renderer.SetRandomColor();
        return human;
    }

    public Human MakePlayer(EcsSonderGameWorld world) {
        world.AddComponent<InputControlled>(_entity);
        _inputControlled = true;
        CurrentRoom.Disabable.SetActive(true);
        _disabable.SetActive(true);
        return this;
    }

    public void TravelTo(Room newRoom) {
        Tr.SetParent(newRoom.Tr);
        var oldRoom = CurrentRoom;
        CurrentRoom = null;
        oldRoom.LocalHumans.Remove(this);

        if (_inputControlled) {
            newRoom.Disabable.SetActive(true);
            oldRoom.Disabable.SetActive(false);
            foreach (var h in oldRoom.LocalHumans) {
                h._disabable.SetActive(false);
            }

            foreach (var h in newRoom.LocalHumans) {
                h._disabable.SetActive(true);
            }
        }

        newRoom.LocalHumans.Add(this);
        if (!_inputControlled) _disabable.SetActive(newRoom.Disabable.Active);
        CurrentRoom = newRoom;
    }
}