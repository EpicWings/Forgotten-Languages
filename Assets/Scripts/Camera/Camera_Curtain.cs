using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Curtain : MonoBehaviour
{
    private RectTransform leftBar, rightBar;

    private void Awake()
    {
        GameObject gameObject = new GameObject("LeftBar", typeof(Image));
        gameObject.transform.SetParent(transform,false);
        gameObject.GetComponent<Image>().color = Color.black;
        leftBar = gameObject.GetComponent<RectTransform>();

        leftBar.anchorMin = new Vector2(0, 1);
        leftBar.anchorMax = new Vector2(1, 1);
        leftBar.sizeDelta = new Vector2(0, 300);

        gameObject = new GameObject("RightBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        rightBar = gameObject.GetComponent<RectTransform>();

        rightBar.anchorMin = new Vector2(1, 0);
        rightBar.anchorMax = new Vector2(1, 1);
        rightBar.sizeDelta = new Vector2(0, 300);
    }
}
