using UnityEngine;

public class RollerSkate : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalSpeed += 1f;
        character.transform.root.GetComponentInChildren<Inventory>().Keep(itemData);
        Remove();
    }

}
