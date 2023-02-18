using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    public Image itemImage;
    public Text itemLevel;

    public event Action<Inventory_Item> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {

    }

    public void Select()
    {

    }
    public void SetData(Sprite sprite, int level)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemLevel.text = level.ToString();
        empty = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (empty)
        {
            return;
        }
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
