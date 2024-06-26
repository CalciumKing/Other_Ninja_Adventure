using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_ManaPotion", menuName = "Items/Mana Potion")]
public class Item_ManaPotion : InventoryItem
{
    public float ManaRestored;

    public override bool UseItem()
    {
        if (GameManager.i.PlayerData.playerMana.CanRestoreMana())
        {
            GameManager.i.PlayerData.playerMana.RestoreMana(ManaRestored);
            return true;
        }
        return false;
    }
}