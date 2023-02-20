using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public Image itemLevelBorder;
    public Text itemLevel;
    public string itemName;
    public UIDescription description;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.Show();
        description.SetDescription(itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.Hide();
    }

    public void SetData(Sprite sprite, int level)
    {
        itemImage.sprite = sprite;
        itemLevel.text = level.ToString();
        itemLevelBorder.gameObject.SetActive(true);
    }

}
