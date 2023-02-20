using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public InventoryPage inventory;
    public InventoryScriptableObj inventoryData;

    private void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();
    }

    private void PrepareUI()
    {
        inventory.InitializeInventory(inventoryData.Size);
        this.inventory.OnDescriptionRequested += HandleDescriptionRequested;
        this.inventory.OnSwapItems += HandleSwapItems;
        this.inventory.OnStartDragging += HandleDragging;
        this.inventory.OnItemActionRequested += HandleItemActionRequested;
    }

    private void HandleItemActionRequested(int itemIndex)
    {

    }

    private void HandleDragging(int itemIndex)
    {
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {

    }

    private void HandleDescriptionRequested(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            return;
        }
        ItemScriptableObj item = inventoryItem.item;
        inventory.UpdateDescription(itemIndex, item.Icon, item.Name, item.Description);
    }

    public void Update()
    {
        if (inventory.isActiveAndEnabled == false)
        {
            inventory.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventory.UpdateData(item.Key, item.Value.item.Icon, item.Value.level);
            }
        }
    }

}
