using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotManager : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Number;

    public void ClearSlot()
    {
        Icon.enabled = false;
        Number.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if(item == null)
        {
            ClearSlot();
            return;
        }

        Icon.enabled = true;
        Number.enabled = true;

        Icon.sprite = item.itemData.Icon;
        Number.text = $"x {item.number:D2}";
    }
}
