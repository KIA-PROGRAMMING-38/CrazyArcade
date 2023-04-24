using UnityEngine;

public class Flask : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalPower += 1;
        character.transform.root.GetComponentInChildren<Inventory>().Keep(itemData);
        Remove();
    }
}
