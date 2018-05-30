using UnityEngine;

[CreateAssetMenu(fileName = "RoomScriptable.asset", menuName = "Create New Room scriptable")]
public class RoomsScriptable : ScriptableObject
{
    public GameObject Hero;
    public GameObject startRoom;
    [Space]
    public GameObject[] rooms;
}