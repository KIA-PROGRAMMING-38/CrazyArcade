using UnityEngine;

public class RollerSkate : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalSpeed += 1f;
        Debug.Log($"스케이트 효과 적용 후 Speed: {character.GetComponent<Status>().Speed}");
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
