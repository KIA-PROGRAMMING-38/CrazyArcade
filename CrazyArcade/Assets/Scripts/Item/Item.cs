using UnityEngine;

public abstract class Item : MonoBehaviour, IPickupable, IRemovable
{
    public abstract void Pickup(GameObject character);

    public abstract void Remove();
}
