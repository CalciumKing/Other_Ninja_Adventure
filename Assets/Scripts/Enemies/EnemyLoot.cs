using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] float xpDropped;
    [SerializeField] DroppedItem[] lootTable;
    public float XPDropped { get { return xpDropped; } }
    public List<DroppedItem> DroppedItems { get; private set; }
    private void Start()
    {
        LoadDroppedItems();
    }
    public void LoadDroppedItems()
    {
        DroppedItems = new List<DroppedItem>();
        foreach (var item in lootTable)
        {
            float prob = Random.Range(0f, 1f);
            if (prob <= item.dropchance)
                DroppedItems.Add(item);
        }
    }
}
[System.Serializable]
public class DroppedItem
{
    public string name;
    public InventoryItem item;
    public int quantity;
    public float dropchance;
    public bool PickedItem { get; set; }
}