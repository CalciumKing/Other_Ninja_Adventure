using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] Image quantityImage;
    [SerializeField] TextMeshProUGUI itemQuantity;

    public int Index { get; set; }

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