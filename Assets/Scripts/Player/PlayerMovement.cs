using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public float speed = 5f;
    public Animator animator;
    public Tilemap obstacles;

    private Vector2 movement;
    private Vector2 moveToPosition;


    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moveToPosition = transform.position + new Vector3(movement.x, movement.y, 0f);
        Vector3Int obstacleTile = obstacles.WorldToCell(moveToPosition - new Vector2(0, 0.5f));

        if (obstacles.GetTile(obstacleTile) == null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            //checking if it is a ledge
            if ((obstacles.GetTile(obstacleTile).name == "base_out_atlas_296") && (movement.y == -1) || (movement.x == -1))
            {

            }

        }


    }

    void FixedUpdate()
    {
        //Normalizing the vector to get a constant speed on all directions
        playerBody.MovePosition(playerBody.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(moveToPosition - new Vector2(0, 0.5f), 0.2f);
    }
}
