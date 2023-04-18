﻿using UnityEngine;

public class Needle : Item, IEquipable
{
    public override void Pickup(GameObject character)
    {
        Equip(character);
        Remove();
    }

    public void Equip(GameObject character)
    {
        // TODO: 인벤토리 구현 후 표시되도록 함
        character.transform.root.GetComponentInChildren<Inventory>().Add(itemData);
        Debug.Log($"{character.name}: 바늘 획득");
    }

}
