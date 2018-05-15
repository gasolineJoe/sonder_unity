using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door destination;

    public void ConnectTo(Door door)
    {
        if (destination == null && door.destination == null)
        {
            destination = door;
            door.destination = this;
        }
        else
        {
            //throw (new System.Exception("This door is already connected to "+destination+"! Use DropConnection if you know what you are doing, smartass"));
        }
    }

    public void DropConnection()
    {
        destination.destination = null;
        destination = null;
    }
}