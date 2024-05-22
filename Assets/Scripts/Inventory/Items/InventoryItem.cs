using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    POTION,
    SCROLL,
    INGREDIENTS,
    TREASURE
}

[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    public string ID, Name;
    [TextArea] public string Description;
    public Sprite Icon;

    public ItemType Type;
    public bool IsConsumable, IsStackable;
    public int MaxStack;
    [HideInInspector] public int Quantity;

    public InventoryItem CreateItem()
    {
        InventoryItem item  = Instantiate(this);
        return item;
    }
    public virtual bool UseItem()
    {
        return true;
    }
    public virtual void EquipItem()
    {
        
    }
    public virtual void RemoveItem()
    {

    }
}