using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDescription : MonoBehaviour
{
    public Image descriptionImage;
    public Text descriptionText;

    public void Show()
    {
        descriptionImage.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        descriptionImage.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }

    public void SetDescription(string text)
    {
        descriptionText.text = text;
    }
}
