using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public RectTransform canvas;
    public UIItem itemPrefab;
    public UIDescription descriptionOfItem;

    List<UIItem> listOfItems = new List<UIItem>();

    public void InitializeInventory(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIItem item = Instantiate(itemPrefab, transform);
            item.Default();
            listOfItems.Add(item);
            item.OnItemHoverOn += ShowDescription;
            item.OnItemHoverOff += HideDescription;
            item.OnItemBeginDrag += BeginDrag;
            item.OnItemEndDrag += EndDrag;
            item.OnItemDroppedOn += DropItem;
        }
    }

    public void AddItem(Sprite sprite, int level, string description)
    {
        var item = Instantiate(itemPrefab, canvas.transform);
        item.OnItemHoverOn += ShowDescription;
        item.OnItemHoverOff += HideDescription;
        item.OnItemBeginDrag += BeginDrag;
        item.OnItemEndDrag += EndDrag;
        item.OnItemDroppedOn += DropItem;
        item.SetData(sprite, level, description);
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

    private void DropItem(UIItem obj)
    {
        throw new NotImplementedException();
    }

    private void EndDrag(UIItem obj)
    {
        throw new NotImplementedException();
    }

    private void BeginDrag(UIItem obj)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        foreach (UIItem item in listOfItems)
        {
            Destroy(item.gameObject);
        }
    }
}
