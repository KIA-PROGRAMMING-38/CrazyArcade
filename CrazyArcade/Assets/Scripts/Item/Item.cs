using UnityEngine;
using UnityEngine.Pool;

public abstract class Item : MonoBehaviour, IPickupable, IRemovable
{
    public ItemData itemData;
    public virtual void Pickup(GameObject character)
    {
        AudioManager.Instance.PlaySFX("pickup_item");
    }

    public virtual void Remove()
    {
        if (ItemPool != null)
        {
            if (gameObject.activeSelf == true)
            {
                ItemPool.Release(this);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public IObjectPool<Item> ItemPool;
    public void SetPool(IObjectPool<Item> pool)
    {
        ItemPool = pool;
    }
}
