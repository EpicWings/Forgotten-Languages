using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fence_Collider : MonoBehaviour
{
    public GameObject Region;
    public Rigidbody2D player;
    public TilemapCollider2D fence_on;
    public TilemapCollider2D fence_off;

    private Bounds boundsRegion;

    private void Start()
    {
        boundsRegion = Region.GetComponent<BoxCollider2D>().bounds;
    }

    private void Update()
    {
        if (boundsRegion.Contains(player.position))
        {
            fence_on.enabled = true;
            fence_off.enabled = false;
        }
        else
        {
            fence_on.enabled = false;
            fence_off.enabled = true;
        }
    }
}
