using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform container;
    [SerializeField] GameObject inventoryPanel;
    private List<InventorySlot> slotList = new List<InventorySlot>();

    private void Start()
    {
        InitInventory();
        InventorySlot.OnSlotSelected += SlotSelected;
    }
    public InventorySlot CurrentSlot { get; set; }
    void SlotSelected(int index)
    {
        CurrentSlot = slotList[index];
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
    public void UseItem()
    {
        Inventory.i.UseItem(CurrentSlot.Index);
    }
    public void DrawSlot(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}