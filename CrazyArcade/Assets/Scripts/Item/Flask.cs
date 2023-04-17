using UnityEngine;

public class Flask : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalPower += 1;
        Debug.Log($"플라스크 효과 적용 후 Power: {character.GetComponent<Status>().Power}");
        character.transform.root.GetComponentInChildren<Inventory>().Keep(itemData);
        Remove();
    }

    public override void Remove()
    {
        if(ItemPool != null)
        {
            ItemPool.Release(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
