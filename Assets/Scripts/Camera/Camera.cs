using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public const short DefaultCameraZ = -10;
    
    public Transform focus;
    public Transform player;

    void Update()
    {
        Debug.Log(player.position.x);
        Debug.Log(player.position.y);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(focus.position.x, focus.position.y, DefaultCameraZ); 

        if ((player.position.x > focus.position.x + 11 || player.position.x < focus.position.x - 10) ||
           (player.position.y > focus.position.y + 7.5 || player.position.y < focus.position.y - 7.5))
        {
           transform.position = new Vector3(player.position.x, player.position.y,DefaultCameraZ);
        }

    }
}
