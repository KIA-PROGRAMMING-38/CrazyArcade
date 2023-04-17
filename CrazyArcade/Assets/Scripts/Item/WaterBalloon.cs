using UnityEngine;

public class WaterBalloon : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalCount += 1;
        Debug.Log($"물풍선 효과 적용 후 Count: {character.GetComponent<Status>().Count}");
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

