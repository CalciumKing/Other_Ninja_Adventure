using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemQuantity;

    public DroppedItem LoadedItem { get; private set; }

    public void SetData(DroppedItem droppedItem)
    {
        LoadedItem = droppedItem;
        itemIcon.sprite = droppedItem.item.Icon;
        itemName.text = droppedItem.item.Name;
        itemQuantity.text = droppedItem.quantity.ToString();
    }

    public void CollectItem()
    {
        if (LoadedItem == null)
            return;

        Inventory.i.AddItem(LoadedItem.item, LoadedItem.quantity);
        LoadedItem.PickedItem = true;
        Destroy(gameObject);
    }
}