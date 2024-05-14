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
            AddItem(testItem, 15);
        }
    }

    private void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0)
            return;
        List<int> itemIndexes = CheckItemStock(item.ID);
        if(item.IsStackable && itemIndexes.Count > 0 )
        {
            foreach(int index in itemIndexes)
            {
                int currentMaxStack = item.MaxStack;
                if (invItems[index].Quantity < currentMaxStack)
                {
                    invItems[index].Quantity += quantity;
                    if (invItems[index].Quantity > currentMaxStack)
                    {
                        int diff = invItems[index].Quantity - currentMaxStack;
                        invItems[index].Quantity = currentMaxStack;
                        AddItem(item, diff);
                    }
                    InventoryUI.i.DrawSlot(invItems[index], index);
                }
            }
        }
        int quantityToAdd = (quantity > item.MaxStack) ? item.MaxStack : quantity;
        AddItemToFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if(remainingAmount > 0)
            AddItem(item, remainingAmount);
    }
    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < invItems.Length; i++)
        {
            if (invItems[i] == null)
                continue;
            if (invItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }
    private void AddItemToFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < invSize; i++)
        {
            if (invItems[i] != null)
                continue;
            invItems[i] = item.CreateItem();
            invItems[i].Quantity = quantity;
            InventoryUI.i.DrawSlot(invItems[i], i);
            return;
        }
    }
}