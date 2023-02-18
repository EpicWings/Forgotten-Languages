using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public InventoryPage inventory;

    private int inventorySize = 8;

    private void Start()
    {
        inventory.InitializeInventory(inventorySize);
    }

    public void Update()
    {
        inventory.Show();
    }

}
