using UnityEngine;

public class RollerSkate : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalSpeed += 0.5f;
        Debug.Log($"스케이트 효과 적용 후 Speed: {character.GetComponent<Status>().Speed}");
        Remove();
    }

    public override void Remove()
    {
        gameObject.SetActive(false);
    }
}
