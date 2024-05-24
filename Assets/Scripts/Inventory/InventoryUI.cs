using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform container;
    [SerializeField] GameObject inventoryPanel;
    private List<InventorySlot> slotList = new List<InventorySlot>();

    [SerializeField] GameObject descriptionPanel;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    public override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        InventorySlot.OnSlotSelected += SlotSelected;
        InitInventory();
    }
    public InventorySlot CurrentSlot { get; set; }
    private void SlotSelected(int index)
    {
        CurrentSlot = slotList[index];
        ShowItemDesciption(index);
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
        if (CurrentSlot == null)
            return;
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
        if (!inventoryPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }
    public void RemoveItem()
    {
        if (CurrentSlot == null)
            return;
        Inventory.i.RemoveItem(CurrentSlot.Index);
    }
    public void EquipItem()
    {
        if (CurrentSlot == null)
            return;

        Inventory.i.EquipItem(CurrentSlot.Index);
    }
    public void ShowItemDesciption(int index)
    {
        if (Inventory.i.InventoryItems[index] == null)
            return;
        descriptionPanel.SetActive(true);
        itemIcon.sprite = Inventory.i.InventoryItems[index].Icon;
        itemName.text = Inventory.i.InventoryItems[index].Name;
        itemDescription.text = Inventory.i.InventoryItems[index].Description;
    }
}