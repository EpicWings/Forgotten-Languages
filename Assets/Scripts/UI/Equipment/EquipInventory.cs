using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipInventory : MonoBehaviour, IDropHandler
{
    public UIInventory inventory;
    public RectTransform canvas;
    public UIItem itemPrefab;
    public UIDescription descriptionOfItem;
    public DragDrop dragDrop;
    public event EventHandler<ItemDroppedEventArgs> OnItemDropped;
    public class ItemDroppedEventArgs : EventArgs
    {
        public UIItem item;
    }
    public EquipInventory equipInventory;

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
            equipInventory.OnItemDropped += DropItem;
        }
    }

    public void AddItem(Sprite sprite, int level, string name, bool stackable, string description, int max)
    {
        var item = Instantiate(itemPrefab, canvas.transform);
        item.OnItemHoverOn += ShowDescription;
        item.OnItemHoverOff += HideDescription;
        item.SetData(sprite, level, name, stackable, description, max);
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



    private void EquipItem(int equipIndex, int inventoryIndex)
    {
        if (equipIndex >= 0 && equipIndex < listOfEquip.Count)
        {
            if (inventoryIndex >= 0 && inventoryIndex < inventory.listOfItems.Count)
            {
                UIItem equipItem = listOfEquip[equipIndex];
                UIItem inventoryItem = inventory.listOfItems[inventoryIndex];

                equipItem.SetData(inventoryItem.itemImage.sprite, 2, inventoryItem.itemName, inventoryItem.isStackable, inventoryItem.itemDescription, inventoryItem.maxStack);
            }
        }
    }

    public void Reset()
    {
        foreach (UIItem item in listOfEquip)
        {
            Destroy(item.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            UIItem item = eventData.pointerDrag.GetComponent<UIItem>();
            if (item != null)
            {
                if (item.itemImage.sprite != null)
                {
                    int equipIndex = listOfEquip.FindIndex(x => x.itemName == item.itemName);
                    int inventoryIndex = inventory.listOfItems.FindIndex(x => x.itemName == item.itemName);
                    Debug.Log("EquipIndex: " + equipIndex);
                    Debug.Log("InventoryIndex: " + inventoryIndex);
                    if (equipIndex != -1)
                    {
                        EquipItem(equipIndex, inventoryIndex);
                    }
                    else
                    {
                        Debug.Log("No such item in equipment");
                    }
                }
            }
        }
    }

    private void DropItem(object sender, ItemDroppedEventArgs e)
    {
        Debug.Log(e.item.itemName);
        /*if (e.item.itemName == "Helmet")
        {
            EquipItem(0, inventory.currentIndex);
        }
        else if (e.item.itemName == "Chest")
        {
            EquipItem(1, inventory.currentIndex);
        }
        else if (e.item.itemName == "Boots")
        {
            EquipItem(2, inventory.currentIndex);
        }
        else if (e.item.itemName == "Weapon")
        {
            EquipItem(3, inventory.currentIndex);
        }
        else if (e.item.itemName == "Ring")
        {
            EquipItem(4, inventory.currentIndex);
        }
        else if (e.item.itemName == "Necklace")
        {
            EquipItem(5, inventory.currentIndex);
        }
        else if (e.item.itemName == "Potion")
        {
            EquipItem(6, inventory.currentIndex);
        }
        else if (e.item.itemName == "Shield")
        {
            EquipItem(7, inventory.currentIndex);
        }
        else if (e.item.itemName == "Bracelet")
        {
            EquipItem(8, inventory.currentIndex);
        }*/
    }

}
