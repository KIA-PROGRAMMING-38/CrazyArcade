using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> InventorySlots = new List<InventorySlot>(6);

    void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        InventorySlots = new List<InventorySlot>(6);
    }

    public void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for(int i = 0; i < InventorySlots.Capacity; ++i)
        {
            CreateInventorySlot();
        }

        for(int i = 0; i < inventory.Count; ++i)
        {
            InventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        InventorySlots.Add(newSlotComponent);
    }
}
