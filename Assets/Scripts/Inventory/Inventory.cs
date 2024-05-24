using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] int invSize;
    [SerializeField] InventoryItem[] invItems;
    [SerializeField] InventoryItem testItem;
    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";
    [SerializeField] GameContent gameContent;

    public int InventorySize => invSize;
    public InventoryItem[] InventoryItems => invItems;

    private void Start()
    {
        invItems = new InventoryItem[invSize];
        CheckSlotForItem();
        LoadInventory();
        //SaveGame.Delete(INVENTORY_KEY_DATA);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 1);
        }
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0)
            return;
        List<int> itemIndexes = CheckItemStock(item.ID);
        if (item.IsStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
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
                    SaveInventory();
                    return;
                }
            }
        }
        int quantityToAdd = (quantity > item.MaxStack) ? item.MaxStack : quantity;
        AddItemToFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
            AddItem(item, remainingAmount);
        SaveInventory();
    }
    private void DecreaseItem(int index)
    {
        invItems[index].Quantity--;

        if (invItems[index].Quantity <= 0)
        {
            invItems[index] = null;
            InventoryUI.i.DrawSlot(null, index);
        }
        else
        {
            InventoryUI.i.DrawSlot(invItems[index], index);
        }
    }
    public void RemoveItem(int index)
    {
        if (invItems[index] == null)
            return;

        invItems[index].RemoveItem();
        invItems[index] = null;
        InventoryUI.i.DrawSlot(null, index);
        SaveInventory();
    }
    public void UseItem(int index)
    {
        if (invItems[index] == null)
            return;

        if (invItems[index].UseItem())
            DecreaseItem(index);
        SaveInventory();
    }
    public void EquipItem(int index)
    {
        if (invItems[index] == null)
            return;
        if (invItems[index].Type != ItemType.WEAPON)
            return;

        invItems[index].EquipItem();
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
    private void CheckSlotForItem()
    {
        for (int i = 0; i < invSize; i++)
        {
            if (invItems[i] == null)
            {
                InventoryUI.i.DrawSlot(null, i);
            }
        }
    }
    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[invSize];
        saveData.ItemQuantity = new int[invSize];

        for (int i = 0; i < invSize; i++)
        {
            if (invItems[i] == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = invItems[i].ID;
                saveData.ItemQuantity[i] = invItems[i].Quantity;
            }
        }
        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }
    private InventoryItem ItemInGameContent(string itemID)
    {
        for (int i = 0; i < invSize; i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }
        return null;
    }
    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < invSize; i++)
            {
                if (loadData.ItemContent[i] != null)
                {
                    InventoryItem itemFromContent = ItemInGameContent(loadData.ItemContent[i]);
                    if (itemFromContent != null)
                    {
                        invItems[i] = itemFromContent.CreateItem();
                        invItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.i.DrawSlot(invItems[i], i);
                    }
                    else
                    {
                        invItems[i] = null;
                    }
                }
            }
        }
    }
}