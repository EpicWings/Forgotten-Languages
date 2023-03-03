using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject character;

    private void Update()
    {
        SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
        image.sprite = spriteRenderer.sprite;
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("UIEnter", true);
        }
    }
}
