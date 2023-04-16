using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public void ClearSlot()
    {
        icon.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if(item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;

        icon.sprite = item.itemData.Icon;
    }
}
