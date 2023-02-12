using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Camera_Movement : MonoBehaviour
{
    public Transform target;

    public Tilemap map;

    private float viewportHeight;
    private float viewportWidth;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Start()
    {
        viewportHeight = Camera.main.orthographicSize * 2f;
        viewportWidth = viewportHeight * Camera.main.aspect;

        minX = map.localBounds.min.x + viewportWidth / 2f;
        maxX = map.localBounds.max.x - viewportWidth / 2f;
        minY = map.localBounds.min.y + viewportHeight / 2f;
        maxY = map.localBounds.max.y - viewportHeight / 2f;
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
