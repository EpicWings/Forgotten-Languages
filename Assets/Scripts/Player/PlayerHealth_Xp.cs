using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_Xp : MonoBehaviour
{
    public HealthBar healthBar;
    public XpBar xpBar;

    public int maxHealth = 100;
    public int currentHealth;
    public int maxXP = 100;
    public int currentXP;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentXP = 0;
        xpBar.SetMaxXp(maxXP);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddXp(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void AddXp(int xp)
    {
        currentXP += xp;
        xpBar.SetXp(currentXP);
    }
}
