using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Image itemImage;
    public Image itemLevelBorder;
    public Text itemLevel;
    public string itemName;
    public bool isStackable;
    public string itemDescription;
    public int maxStack;

    public event Action<UIItem> OnItemHoverOn, OnItemHoverOff,/*OnItemClicked,*/ OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnItemHoverOn?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnItemHoverOff?.Invoke(this);
    }

    public void SetData(Sprite sprite, int level, string description)
    {
        itemImage.sprite = sprite;
        itemImage.color = new Color(255, 255, 255, 255);
        itemLevel.text = level.ToString();
        itemLevelBorder.gameObject.SetActive(true);
        itemDescription = description;

        empty = false;
    }

    public void Default()
    {
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemLevel.text = "";
        itemLevelBorder.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!empty)
        {
            OnItemBeginDrag?.Invoke(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }
}
