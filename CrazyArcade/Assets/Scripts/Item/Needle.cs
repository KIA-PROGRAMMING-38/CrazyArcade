using UnityEngine;

public class Needle : Item, IEquipable
{
    public void Equip(GameObject character)
    {
        Debug.Log($"{character.name}장착 바늘");
    }

    public override void Pickup(GameObject character)
    {
        Equip(character);
    }
}
