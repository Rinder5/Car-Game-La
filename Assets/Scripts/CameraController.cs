using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    
    // Use this for initialization
    void Start()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        if (player.transform.position.y - transform.position.y >= -0.5 && transform.position.y <= 33)
        {
            transform.position += new Vector3(0,(float)0.02,0);
            if (player.transform.position.y - transform.position.y >= 0)
            {
                transform.position += new Vector3(0, (float)0.03, 0);
                if (player.transform.position.y - transform.position.y >= 0.5)
                    transform.position += new Vector3(0, (float)0.05, 0);
            }
        }
        else if (player.transform.position.y - transform.position.y < -2 && transform.position.y > 0)
        {
            transform.position += new Vector3(0,(float)-0.02,0);
            if (player.transform.position.y - transform.position.y < -2.5)
            {
                transform.position += new Vector3(0, (float)-0.03, 0);
                if (player.transform.position.y - transform.position.y < -3)
                    transform.position += new Vector3(0, (float)-0.05, 0);
            }
        }

    }
}