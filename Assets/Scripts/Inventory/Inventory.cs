using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] int invSize;
    [SerializeField] InventoryItem[] invItems;
    [SerializeField] InventoryItem testItem;

    public int InventorySize => invSize;

    private void Start()
    {
        invItems = new InventoryItem[invSize];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            invItems[0] = testItem.CreateItem();
            invItems[0].Quantity = 10;

            InventoryUI.i.DrawSlot(invItems[0], 0);
        }
    }
}