using UnityEngine;

public abstract class Usable : MonoBehaviour {
    public enum UsableType { Door, Locker }
    public abstract UsableType Identify();
}
