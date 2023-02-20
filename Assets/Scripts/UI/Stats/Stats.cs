using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Stats : MonoBehaviour
{
    public Text stats;

    public const string spacing = "    ";

    public void SetStats(HealthBar health)
    {
        stats.text = spacing + "MaxHp : " + health.slider.maxValue + "\n";
        /* "Health Regeneration" + regen + "\n" +
         "Attack" + attack + "\n" +
         "Defense" + defense + "\n" +
         "Attack Speed" + attackSpeed + "\n" +
         "Critical Chance" + criticalChance + "\n" +
         "Life Steal" + lifeSteal + "\n";*/
    }

}
