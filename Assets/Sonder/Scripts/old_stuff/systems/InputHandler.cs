using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField]
    float xAxis, yAxis;
    [SerializeField]
    Person controlledPerson;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            controlledPerson?.OnUse();
    }

    public void SetControlledPerson(Person controlledPerson)
    {
        this.controlledPerson = controlledPerson;
    }
}
