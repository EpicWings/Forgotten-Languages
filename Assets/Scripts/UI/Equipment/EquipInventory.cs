using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventory : MonoBehaviour
{
    public UIInventory inventory;
    public RectTransform canvas;
    public UIItem itemPrefab;
    public UIDescription descriptionOfItem;
    public DragDrop dragDrop;

    private int currentIndex = -1;
    private List<UIItem> listOfEquip = new List<UIItem>();
    private readonly List<string> EquipmentOrder = new()
    {
        "Helmet",
        "Chest",
        "Boots",
        "Weapon",
        "Ring",
        "Necklace",
        "Potion",
        "Shield",
        "Bracelet"
    };

    private void Awake()
    {
        dragDrop.Toggle(false);
    }

    public void InitializeEquipment(int size = 9)
    {
        for (int i = 0; i < size; i++)
        {
            UIItem item = Instantiate(itemPrefab, transform);
            item.Default();
            item.itemName = EquipmentOrder[i];
            listOfEquip.Add(item);
            item.OnItemHoverOn += ShowDescription;
            item.OnItemHoverOff += HideDescription;
            item.OnItemDrag += DragItems;
            item.OnItemBeginDrag += BeginDrag;
            item.OnItemEndDrag += EndDrag;
            item.OnItemDroppedOn += DropItem;
            item.OnItemClicked += ClickItem;
        }
    }

    public void AddItem(Sprite sprite, int level, string description)
    {
        var item = Instantiate(itemPrefab, canvas.transform);
        item.OnItemHoverOn += ShowDescription;
        item.OnItemHoverOff += HideDescription;
        item.OnItemDrag += DragItems;
        item.OnItemBeginDrag += BeginDrag;
        item.OnItemEndDrag += EndDrag;
        item.OnItemDroppedOn += DropItem;
        item.OnItemClicked += ClickItem;
        item.SetData(sprite, level, description);
        listOfEquip.Add(item);
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
        Debug.Log("Dropped");
        int index1 = listOfEquip.IndexOf(obj);

        if (listOfEquip[index1].itemName == inventory.listOfItems[inventory.currentIndex].itemName)
        {
            Debug.Log("Dropped on the same item");
            return;
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
        currentIndex = listOfEquip.IndexOf(obj);
        dragDrop.Toggle(true);
        dragDrop.SetData(obj.itemImage.sprite, Convert.ToInt32(obj.itemLevel.text), obj.itemDescription);
    }

    private void SwapItems(int index1, int index2)
    {
        UIItem item1 = listOfEquip[index1];
        UIItem item2 = listOfEquip[index2];

        listOfEquip[index1] = item2;
        listOfEquip[index2] = item1;

        item1.transform.SetSiblingIndex(index2);
        item2.transform.SetSiblingIndex(index1);
    }

    public void Reset()
    {
        foreach (UIItem item in listOfEquip)
        {
            Destroy(item.gameObject);
        }
    }
}
