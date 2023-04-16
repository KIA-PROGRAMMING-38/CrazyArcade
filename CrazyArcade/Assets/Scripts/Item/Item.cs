using UnityEngine;
using UnityEngine.Pool;

public abstract class Item : MonoBehaviour, IPickupable, IRemovable
{
    public ItemData itemData;
    public abstract void Pickup(GameObject character);

    public abstract void Remove();

    private IObjectPool<Item> ItemPool;
    public void SetPool(IObjectPool<Item> pool)
    {
        ItemPool = pool;
    }
}
