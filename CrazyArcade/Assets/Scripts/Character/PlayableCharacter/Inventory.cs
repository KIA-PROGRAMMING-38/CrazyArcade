using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> HoldingItems = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Needle.OnPickUpNeedle += Add;
    }

    private void OnDisable()
    {
        Needle.OnPickUpNeedle -= Add;
    }

    public void Add(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToInventory();
            OnInventoryChange?.Invoke(HoldingItems);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            HoldingItems.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            OnInventoryChange?.Invoke(HoldingItems);
        }
    }

    public void Remove(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromInventory();
            if(item.number == 0)
            {
                HoldingItems.Remove(item);
                itemDictionary.Remove(itemData);
            }

            OnInventoryChange?.Invoke(HoldingItems);
        }
    }
}
