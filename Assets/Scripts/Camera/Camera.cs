using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Camera : MonoBehaviour
{
    public const short DefaultCameraZ = -10;
    
    public Transform player;
    public Transform[] arrayTransform;

    public Vector2 closestPoint;


    private void Start()
    {
        closestPoint = ClosestInRange();
        transform.position = new Vector3(closestPoint.x, closestPoint.y, DefaultCameraZ);
    }

    private void Update()
    {
        closestPoint = ClosestInRange();
    }
    private void FixedUpdate()
    {
        if ((player.position.x > closestPoint.x + 11 || player.position.x < closestPoint.x - 10) ||
           (player.position.y > closestPoint.y + 7.5 || player.position.y < closestPoint.y - 7.5))
        {
            transform.position = new Vector3(ClosestInRange().x, ClosestInRange().y, DefaultCameraZ);
        }
    }
    /*
     * if ((player.position.x > closestPoint.x + 11 || player.position.x < closestPoint.x - 10) ||
           (player.position.y > closestPoint.y + 7.5 || player.position.y < closestPoint.y - 7.5))
        {
            transform.position = new Vector3(ClosestInRange().x, ClosestInRange().y, DefaultCameraZ);
        }
     */
    /*void FixedUpdate()
    {

        
    }*/

    Vector2 ClosestInRange()
    {
        Vector2 closest = new(float.MaxValue, float.MaxValue);

        for (int i = 0; i < arrayTransform.Length; i++)
        {
            if (Mathf.Abs(player.position.x - arrayTransform[i].position.x) < Mathf.Abs(player.position.x - closest.x) &&
                Mathf.Abs(player.position.y - arrayTransform[i].position.y) < Mathf.Abs(player.position.y - closest.y))
            {
                closest = arrayTransform[i].position;
            }
        }
        

        return closest;
    }

}
