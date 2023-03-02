using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas UICanvas;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        UICanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        UICanvas.gameObject.SetActive(false);
    }
}
