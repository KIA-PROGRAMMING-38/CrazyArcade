using UnityEngine;

public class Needle : Item, IEquipable
{
    public static event PickUpNeedle OnPickUpNeedle;
    public delegate void PickUpNeedle(ItemData itemData);
    public override void Pickup(GameObject character)
    {
        Equip(character);
        OnPickUpNeedle?.Invoke(itemData);
        Remove();
    }

    public void Equip(GameObject character)
    {
        // TODO: 인벤토리 구현 후 표시되도록 함
        Debug.Log($"{character.name}: 바늘 획득");
    }

    public override void Remove()
    {
        gameObject.SetActive(false);
    }
}
