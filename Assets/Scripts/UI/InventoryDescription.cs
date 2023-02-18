using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    public Text description;

    private void Awake()
    {
        ResetDescription();

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ResetDescription()
    {
        description.text = "";
    }

    public void SetDescription(string itemName, string itemDescription)
    {
        this.description.text = itemDescription;
    }


}
