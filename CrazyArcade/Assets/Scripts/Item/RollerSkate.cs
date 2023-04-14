using UnityEngine;

public class RollerSkate : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalSpeed += 0.5f;
    }
}
