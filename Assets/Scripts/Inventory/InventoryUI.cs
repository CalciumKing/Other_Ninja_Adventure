using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform container;
    private List<InventorySlot> slotList = new List<InventorySlot>();

    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.i.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }
    public void DrawSlot(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
}