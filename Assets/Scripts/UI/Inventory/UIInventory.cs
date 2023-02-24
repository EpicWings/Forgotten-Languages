using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public RectTransform canvas;
    public UIItem itemPrefab;
    public UIDescription descriptionOfItem;
    public DragDrop dragDrop;
    public int currentIndex = -1;
    public List<UIItem> listOfItems = new List<UIItem>();

    private void Awake()
    {
        dragDrop.Toggle(false);
    }

    public void InitializeInventory(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIItem item = Instantiate(itemPrefab, transform);
            item.Default();
            listOfItems.Add(item);
            item.OnItemHoverOn += ShowDescription;
            item.OnItemHoverOff += HideDescription;
            item.OnItemDrag += DragItems;
            item.OnItemBeginDrag += BeginDrag;
            item.OnItemEndDrag += EndDrag;
            item.OnItemDroppedOn += DropItem;
            item.OnItemClicked += ClickItem;
        }
    }

    public void AddItem(Sprite sprite, int level, string name, bool stackable, string description, int max)
    {
        var item = Instantiate(itemPrefab, canvas.transform);
        item.OnItemHoverOn += ShowDescription;
        item.OnItemHoverOff += HideDescription;
        item.OnItemDrag += DragItems;
        item.OnItemBeginDrag += BeginDrag;
        item.OnItemEndDrag += EndDrag;
        item.OnItemDroppedOn += DropItem;
        item.OnItemClicked += ClickItem;
        item.SetData(sprite, level, name, stackable, description, max);
        listOfItems.Add(item);
    }


    private void HideDescription(UIItem obj)
    {
        if (obj.itemImage.sprite != null)
        {
            descriptionOfItem.Hide();
            descriptionOfItem.SetDescription("");
        }
    }

    private void ShowDescription(UIItem obj)
    {
        if (obj.itemImage.sprite != null)
        {
            descriptionOfItem.Show();
            descriptionOfItem.SetDescription(obj.itemDescription);
        }
    }

    private void ClickItem(UIItem obj)
    {
        if (obj.itemImage.sprite != null)
        {
        }
    }


    private void DropItem(UIItem obj)
    {
        int index1 = listOfItems.IndexOf(obj);
        if (index1 != -1)
        {
            if (index1 != currentIndex)
            {
                SwapItems(currentIndex, index1);
            }
        }
    }

    private void DragItems(UIItem obj)
    {

    }

    private void EndDrag(UIItem obj)
    {

        dragDrop.Toggle(false);
    }

    private void BeginDrag(UIItem obj)
    {
        currentIndex = listOfItems.IndexOf(obj);
        dragDrop.Toggle(true);
        dragDrop.SetData(obj.itemImage.sprite, Convert.ToInt32(obj.itemLevel.text), obj.itemName, obj.isStackable, obj.itemDescription, obj.maxStack);
    }

    private void SwapItems(int index1, int index2)
    {
        UIItem item1 = listOfItems[index1];
        UIItem item2 = listOfItems[index2];

        listOfItems[index1] = item2;
        listOfItems[index2] = item1;

        item1.transform.SetSiblingIndex(index2);
        item2.transform.SetSiblingIndex(index1);
    }

    public void Reset()
    {
        foreach (UIItem item in listOfItems)
        {
            Destroy(item.gameObject);
        }
    }
}
