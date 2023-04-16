using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryManager _inventoryManager;
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> HoldingItems = new List<InventoryItem>();  // ���Կ� ǥ�õ� ������
    public List<InventoryItem> KeepItems = new List<InventoryItem>();  // ���� ������ ����
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void Awake()
    {
        _inventoryManager = transform.root.GetChild(0).GetChild(0).GetComponent<InventoryManager>();
    }

    public void Add(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToInventory();
            _inventoryManager.DrawInventory(HoldingItems);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            HoldingItems.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            // OnInventoryChange?.Invoke(HoldingItems);
            _inventoryManager.DrawInventory(HoldingItems);
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

            // OnInventoryChange?.Invoke(HoldingItems);
        }
    }
}
