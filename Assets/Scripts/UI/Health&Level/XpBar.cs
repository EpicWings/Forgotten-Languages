using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider slider;
    public Text xpValue;

    public void SetMaxXp(int xp)
    {
        slider.maxValue = xp;
        slider.value = 0;
        xpValue.text = "0" + "/" + xp.ToString();
    }

    public void SetXp(int xp)
    {
        slider.value = xp;
        xpValue.text = xp.ToString() + "/" + slider.maxValue.ToString();
    }

}
