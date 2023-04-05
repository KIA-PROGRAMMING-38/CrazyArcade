using UnityEngine;
using UnityEngine.Pool;

public class BubblePool : MonoBehaviour
{
    public Bubble bubblePrefab;

    public IObjectPool<Bubble> bubblePool;

    private void Awake()
    {
        bubblePool = new ObjectPool<Bubble>
            (
                CreateBubble,
                OnGet,
                OnRelease,
                ActionOnDestroy,
                maxSize: 10
            );
    }

    private Bubble CreateBubble()
    {
        Bubble bubble = Instantiate(bubblePrefab);
        bubble.SetPool(bubblePool);
        return bubble;
    }

    private void OnGet(Bubble bubble)
    {
        bubble.gameObject.SetActive(true);
    }

    private void OnRelease(Bubble bubble)
    {
        bubble.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bubble bubble)
    {
        Destroy(bubble.gameObject);
    }
}
