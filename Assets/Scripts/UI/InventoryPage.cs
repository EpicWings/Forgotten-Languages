using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPage : MonoBehaviour
{
    public Inventory_Item itemPrefab;
    public RectTransform contentPanel;
    public InventoryDescription description;
    public DragItem dragItem;

    public Sprite image, image2;
    public int level;
    public string itemDescription;

    private int currentDraggedItemIndex = -1;

    List<Inventory_Item> listOfItems = new List<Inventory_Item>();

    private void Awake()
    {
        dragItem.Toggle(false);
        description.ResetDescription();
    }

    public void InitializeInventory(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Inventory_Item item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemEndDrag += HandleEndDrag;

        }
    }

    public void HandleItemSelection(Inventory_Item inventoryItem)
    {
        description.SetDescription(inventoryItem.name, itemDescription);
        //print item index
        Debug.Log(listOfItems.IndexOf(inventoryItem));

    }

    public void HandleEndDrag(Inventory_Item inventoryItem)
    {
        dragItem.Toggle(false);
    }

    public void HandleSwap(Inventory_Item inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            dragItem.Toggle(false);
            currentDraggedItemIndex = -1;
            return;
        }
        listOfItems[currentDraggedItemIndex].SetData(index == 0 ? image : image2, level);
        listOfItems[index].SetData(currentDraggedItemIndex == 0 ? image : image2, level);
        dragItem.Toggle(false);
        currentDraggedItemIndex = -1;
    }

    public void HandleBeginDrag(Inventory_Item inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        currentDraggedItemIndex = index;
        dragItem.Toggle(true);
        dragItem.SetData(index == 0 ? image : image2, level);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        listOfItems[0].SetData(image, level);
        listOfItems[1].SetData(image2, level);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
