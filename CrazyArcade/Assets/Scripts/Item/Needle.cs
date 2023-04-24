using UnityEngine;

public class Needle : Item, IEquipable
{
    public override void Pickup(GameObject character)
    {
        Equip(character);
        Remove();
    }

    public void Equip(GameObject character)
    {
        character.transform.root.GetComponentInChildren<Inventory>().Add(itemData);
    }
}
