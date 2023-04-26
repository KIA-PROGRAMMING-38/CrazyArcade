using UnityEngine;

public class WaterBalloon : Item
{
    public override void Pickup(GameObject character)
    {
        base.Pickup(character);
        character.GetComponent<Status>().AdditionalCount += 1;
        character.transform.root.GetComponentInChildren<Inventory>().Keep(itemData);
        Remove();
    }
}

