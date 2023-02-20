using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar health;
    public Stats stats;
    void Update()
    {
        stats.SetStats(health);
    }
}
