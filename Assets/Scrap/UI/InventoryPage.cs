using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPage : MonoBehaviour
{
    public UIInventoryItem itemPrefab;
    public RectTransform contentPanel;
    public InventoryDescription description;
    public DragItem dragItem;

    private int currentDraggedItemIndex = -1;

    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        dragItem.Toggle(false);
        description.ResetDescription();
    }

    public void InitializeInventory(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemEndDrag += HandleEndDrag;

        }
    }

    public void UpdateData(int itemIndex, Sprite sprite, int level)
    {
        if (listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(sprite, level);
        }
    }

    public void HandleItemSelection(UIInventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);

    }

    public void HandleEndDrag(UIInventoryItem inventoryItem)
    {
        ResetDragItem();
    }

    public void HandleSwap(UIInventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentDraggedItemIndex, index);
        HandleItemSelection(inventoryItem);
    }

    private void ResetDragItem()
    {
        dragItem.Toggle(false);
        currentDraggedItemIndex = -1;
    }

    public void HandleBeginDrag(UIInventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        currentDraggedItemIndex = index;
        HandleItemSelection(inventoryItem);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDragItem(Sprite sprite, int level)
    {
        dragItem.Toggle(true);
        dragItem.SetData(sprite, level);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    private void ResetSelection()
    {
        description.ResetDescription();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDragItem();
    }

    public void UpdateDescription(int itemIndex, Sprite icon, string name, string text)
    {
        description.SetDescription(name, text);
        listOfItems[itemIndex].Select();
    }
}
