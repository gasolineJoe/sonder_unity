using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object
    
    void LateUpdate()
    {
        if (player != null)
            transform.position = player.transform.position;
        transform.position = new Vector3(transform.position.x+1, transform.position.y+4, -10);
    }
}