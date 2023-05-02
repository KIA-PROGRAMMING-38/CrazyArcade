using UnityEngine;

public class Needle : Item, IEquipable
{
    public override void Pickup(GameObject character)
    {
        base.Pickup(character);
        Remove();
        Equip(character);
    }

    public void Equip(GameObject character)
    {
        character.transform.root.GetComponentInChildren<Inventory>().Add(itemData);
    }
}
