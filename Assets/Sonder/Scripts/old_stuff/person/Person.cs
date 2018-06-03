using UnityEngine;

public class Person : MonoBehaviour {
    public void OnUse()
    {
        GetComponent<DoorUser>()?.OnUse();
    }
}
