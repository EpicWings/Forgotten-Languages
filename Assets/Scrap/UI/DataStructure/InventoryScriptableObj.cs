using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu]
public class InventoryScriptableObj : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 9;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemScriptableObj item, int level)
    {

        if (item.IsStackable == false)
        {
            for (int i = 0; i < 9; i++)
            {
                while (level > 0 && IsInventoryFull() == false)
                {
                    level -= AddItemToFirstFreeSlot(item, 1);
                }
                InformAboutChange();
                return level;
            }
        }

        level = AddStackableItem(item, level);
        InformAboutChange();
        return level;
    }

    private int AddItemToFirstFreeSlot(ItemScriptableObj item, int level)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            level = level,

        };

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return level;
            }
        }
        return 0;
    }

    private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddStackableItem(ItemScriptableObj item, int level)
    {
        return 0;
    }

    public void RemoveItem(int itemIndex)
    {

    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.item, item.level);
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> inventoryState = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            inventoryState[i] = inventoryItems[i];
        }
        return inventoryState;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public void SwapItems(int itemIndex_1, int itemIndex_2)
    {
        InventoryItem item1 = inventoryItems[itemIndex_1];
        inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
        inventoryItems[itemIndex_2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }
}


//we use struct instead of class because we don't want to inherit from it and we don't want to create a reference to it
[Serializable]
public struct InventoryItem
{
    public ItemScriptableObj item;
    public int level;

    public bool IsEmpty => item == null;

    //the value of an item cannot be null
    public static InventoryItem GetEmptyItem() => new InventoryItem { item = null, level = 0 };
}
