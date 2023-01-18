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
        if (transform.position.x != closestPoint.x || transform.position.y != closestPoint.y)
        {
            transform.position = new Vector3(ClosestInRange().x, ClosestInRange().y, DefaultCameraZ);
        }
    }

    public Vector2 ClosestInRange()
    {
        Vector2 closest = new(0, 0);
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < arrayTransform.Length; i++)
        {
            float distance = Vector2.Distance(player.position, arrayTransform[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = arrayTransform[i].position;
            }
        }
        return closest;
    }
}
