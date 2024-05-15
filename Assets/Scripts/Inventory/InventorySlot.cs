using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelected;
    [SerializeField] Image itemIcon;
    [SerializeField] Image quantityImage;
    [SerializeField] TextMeshProUGUI itemQuantity;

    public int Index { get; set; }

    public void ClickSlot()
    {
        OnSlotSelected?.Invoke(Index);
    }

    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon;
        itemQuantity.text = item.Quantity.ToString();
    }

    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value);
        quantityImage.gameObject.SetActive(value);
    }
}