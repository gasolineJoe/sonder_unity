using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public void HadnleInput(float xAxis, float yAxis)
    {
        GetComponent<LocalWalker>()?.WalkInCurrentRoom(xAxis);
    }

    public void OnUse()
    {
        GetComponent<DoorUser>()?.OnUse();
    }
}
