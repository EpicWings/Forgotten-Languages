using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Inventory_Item itemPrefab;
    public RectTransform contentPanel;

    List<Inventory_Item> listOfItems = new List<Inventory_Item>();

    public void InitializeInvetory(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Inventory_Item item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemEndDrag += HandleEndDrag;

        }
    }

    private void HandleItemSelection(Inventory_Item obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleEndDrag(Inventory_Item obj)
    {

    }

    private void HandleSwap(Inventory_Item obj)
    {

    }

    private void HandleBeginDrag(Inventory_Item obj)
    {

    }

}
