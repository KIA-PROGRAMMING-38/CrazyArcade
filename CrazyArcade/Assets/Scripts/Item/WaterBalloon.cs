using UnityEngine;

public class WaterBalloon : Item
{
    public override void Pickup(GameObject character)
    {
        character.GetComponent<Status>().AdditionalCount += 1;
        Debug.Log($"물풍선 효과 적용 후 Count: {character.GetComponent<Status>().Count}");
        Remove();
    }

    public override void Remove()
    {
        gameObject.SetActive(false);
    }
}

