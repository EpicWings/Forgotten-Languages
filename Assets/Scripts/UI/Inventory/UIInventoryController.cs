using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    public UIInventory inventoryPrefab;
    public EquipInventory equipmentPrefab;
    public ItemScriptableObj item;
    public ItemScriptableObj item2;


    private const int maxInventory = 7;
    public void Start()
    {
        equipmentPrefab.descriptionOfItem.Hide();
        inventoryPrefab.descriptionOfItem.Hide();
        equipmentPrefab.InitializeEquipment();
        inventoryPrefab.InitializeInventory(maxInventory);
        inventoryPrefab.AddItem(item.Sprite, 1, item.Description);
        inventoryPrefab.AddItem(item2.Sprite, 1, item2.Description);

    }

    public void Update()
    {

    }
}
