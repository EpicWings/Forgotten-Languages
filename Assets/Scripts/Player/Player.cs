using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private GameObject EnemyParent;
    [SerializeField] private UIController UICanvas;


    private void Update()
    {
        UICanvas.Hide();

        for(int index = 0; index < EnemyParent.transform.childCount; index++)
        {
            Transform enemy = EnemyParent.transform.GetChild(index);
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            
            if(player.IsTouching(enemyCollider))
            {

                UICanvas.Show();
            }
        }
    }
}
