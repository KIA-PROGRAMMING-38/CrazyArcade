using UnityEngine;
using UnityEngine.Pool;

public class ItemPool : MonoBehaviour
{
    public Item[] ItemPrefabs;
    public IObjectPool<Item> ItemsPool;

    private void Awake()
    {
        ItemsPool = new ObjectPool<Item>
            (
                CreateItems,
                OnGet,
                OnRelease,
                ActionOnDestroy,
                defaultCapacity: 5,
                maxSize: 30
            );
    }

    // random 아이템을 생성
    private Item CreateItems()
    {
        int randomIndex = Random.Range(0, ItemPrefabs.Length + 1);

        if(randomIndex == ItemPrefabs.Length)
        {
            return null;
        }

        Item item = Instantiate(ItemPrefabs[randomIndex]);
        item.SetPool(ItemsPool);
        return item;
    }

    private void OnGet(Item item)
    {
        if(item == null)
        {
            return;
        }

        item.gameObject.SetActive(true);
        item.GetComponent<Animator>().SetTrigger("GetFromBlock");
    }

    private void OnRelease(Item item)
    {
        item.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Item item)
    {
        Destroy(item.gameObject);
    }
}
