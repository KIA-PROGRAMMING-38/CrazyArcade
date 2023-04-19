using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private SlotManager _slotManager;

    private List<InventoryItem> _holdingItems = new List<InventoryItem>();  // 슬롯에 표시될 아이템
    private List<InventoryItem> _keepItems = new List<InventoryItem>();  // 스탯 아이템 저장
    private Dictionary<ItemData, InventoryItem> _itemDictionary = new Dictionary<ItemData, InventoryItem>();
    private Dictionary<ItemData, InventoryItem> _keepItemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void Awake()
    {
        _inventoryManager = transform.root.GetChild(0).GetChild(0).GetComponent<InventoryManager>();
        _slotManager = transform.root.GetChild(0).GetChild(1).GetComponent<SlotManager>();
    }

    /// <summary>
    /// 인벤토리 슬롯에 표시하지 않을 아이템들을 관리
    /// </summary>
    /// <param name="itemData"></param>
    public void Keep(ItemData itemData)
    {
        if (_keepItemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToInventory();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            _keepItems.Add(newItem);
            _keepItemDictionary.Add(itemData, newItem);
        }
    }

    /// <summary>
    /// 아이템 추가 후 인벤토리 슬롯에 업데이트
    /// </summary>
    /// <param name="itemData"></param>
    public void Add(ItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToInventory();
            _inventoryManager.DrawInventory(_holdingItems);
            _slotManager.DrawSlot(_holdingItems[0]);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            _holdingItems.Add(newItem);
            _itemDictionary.Add(itemData, newItem);
            _inventoryManager.DrawInventory(_holdingItems);
            _slotManager.DrawSlot(_holdingItems[0]);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromInventory();
            if (item.number == 0)
            {
                _holdingItems.Remove(item);
                _itemDictionary.Remove(itemData);
            }

            _inventoryManager.DrawInventory(_holdingItems);

            if(_holdingItems.Count == 0)
            {
                _slotManager.DrawSlot(null);
                return;
            }
            _slotManager.DrawSlot(_holdingItems[0]);
        }
    }

    int isDying = Animator.StringToHash("Base Layer.Dying.Dying_Start");
    public void UseItem(int selectedSlotIndex, Character useCharacter)
    {
        if(_holdingItems.Count == 0)
        {
            return;
        }

        ItemData useItem = _holdingItems[selectedSlotIndex].itemData;
        if(useItem == null)
        {
            return;
        }

        if (useCharacter.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash != PlayableCharacter.PlayerAnimID.ON_IS_DYING_START)
        {
            return;
        }

        Action<Character> action = ItemActions.itemActions[useItem.Id];
        action(useCharacter);
        Remove(useItem);
    }
}
