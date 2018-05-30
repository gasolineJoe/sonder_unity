using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [SerializeField]
    float xAxis, yAxis;
    [SerializeField]
    Person controlledPerson;

    // Update is called once per frame
    void Update () {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        if (xAxis != 0 || yAxis != 0)
            controlledPerson?.HadnleInput(xAxis, yAxis);

        if (Input.GetKeyDown(KeyCode.F))
            controlledPerson?.OnUse();
    }

    public void SetControlledPerson(Person controlledPerson)
    {
        this.controlledPerson = controlledPerson;
    }
}
