using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Inventory inventory;

    private int inventorySize = 9;

    private void Start()
    {
        inventory.InitializeInvetory(inventorySize);
    }

    public void Update()
    {

    }

}
