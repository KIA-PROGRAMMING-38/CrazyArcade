using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFlask : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().MaxPower = true;
        character.transform.root.GetComponentInChildren<Inventory>().Keep(itemData);
        Remove();
    }
}
