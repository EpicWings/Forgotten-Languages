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
    public EquipInventory equipInventory;
    public List<UIItem> listOfEquip = new List<UIItem>();

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


    public void HideDescription(UIItem obj)
    {
        if (obj.itemImage.sprite != null)
        {
            descriptionOfItem.Hide();
            descriptionOfItem.SetDescription("");
        }
    }

    public void ShowDescription(UIItem obj)
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

                equipItem.SetData(inventoryItem.itemImage.sprite, Convert.ToInt32(inventoryItem.itemLevel),
                                  inventoryItem.itemName, inventoryItem.isStackable, inventoryItem.itemDescription, inventoryItem.maxStack);
                RemoveAlpha(equipItem);

                inventoryItem.Default();
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

    private void RemoveAlpha(UIItem item)
    {
        CanvasGroup canvasGroup = item.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        item.itemImage.color = new Color(255, 255, 255, 255);

    }
}

