using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public const short DefaultCameraZ = -10;
    
    public Transform player;
    public Transform[] arrayTransform = new Transform[2];

    public Vector2 closestPoint;

    void Update()
    {
        /*Debug.Log(player.position.x);
        Debug.Log(player.position.y);*/
        Debug.Log(closestInRange().x);
        Debug.Log(closestInRange().y);
        closestPoint = closestInRange();
    }

    void FixedUpdate()
    {
        
        transform.position = new Vector3(closestPoint.x, closestPoint.y, DefaultCameraZ);
        
        if ((player.position.x > transform.position.x + 11 || player.position.x <transform.position.x - 10) ||
           (player.position.y > transform.position.y + 7.5 || player.position.y <transform.position.y - 7.5))
        {
           transform.position = new Vector3(closestInRange().x, closestInRange().y, DefaultCameraZ);
        }

    }
    Vector2 closestInRange()
    {
        Vector2 closest = new(100,100);

        for (int i = 0; i < arrayTransform.Length; i++)
        {
            Vector2 aux;
            aux.x = Mathf.Abs(arrayTransform[i].position.x - player.position.x); aux.y = Mathf.Abs(arrayTransform[i].position.y - player.position.y); 

            if (aux.x < closest.x && aux.y < closest.y)
            {
                closest = arrayTransform[i].position;
            }
        }

        return closest;
    }
    
}
