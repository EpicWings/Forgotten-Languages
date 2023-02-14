using DG.Tweening;
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
    public Collider2D ledgeCollider;
    public Collider2D playerCollider;

    private Vector2 movement;
    private Vector2 moveToPosition;



    private void Update()
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
            //Debug.Log(obstacles.GetTile(obstacleTile).name);

            //checking if it is a ledge
            switch (obstacles.GetTile(obstacleTile).name)
            {
                case "base_out_atlas_296":
                    if ((movement.y == -1 || movement.x == -1) && Input.GetKeyDown(KeyCode.Space))
                    {
                        StartCoroutine(DisableCollider(ledgeCollider, 1f));
                        StartCoroutine(Jump(movement));
                    }
                    break;
                case "terrain_atlas_300":
                    if ((movement.x == -1 || movement.y == -1) && Input.GetKeyDown(KeyCode.Space))
                    {
                        StartCoroutine(DisableCollider(ledgeCollider, 1f));
                        StartCoroutine(Jump(movement));
                    }
                    break;
                case null:
                    ledgeCollider.enabled = true;
                    break;
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

    IEnumerator DisableCollider(Collider2D collider, float time)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }

    IEnumerator Jump(Vector2 direction)
    {
        float multiplier;

        if (direction.x == -1)
            multiplier = 1.5f;
        else
            multiplier = 2.5f;

        var jumpDest = transform.position + new Vector3(direction.x, direction.y) * multiplier;
        yield return transform.DOJump(jumpDest, 0.3f, 1, 0.5f).WaitForCompletion();
    }
}
