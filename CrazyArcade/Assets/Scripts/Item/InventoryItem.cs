using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int number;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        AddToInventory();
    }

    public void AddToInventory()
    {
        ++number;
    }

    public void RemoveFromInventory()
    {
        --number;
    }
}
