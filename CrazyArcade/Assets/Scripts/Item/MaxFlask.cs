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

    public override void Remove()
    {
        if (ItemPool != null)
        {
            ItemPool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
