using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BubblePool : MonoBehaviour
{
    public Bubble bubblePrefab;
    public GameObject Player;
    public IObjectPool<Bubble> bubblePool;

    private void Awake()
    {
        Player = transform.root.gameObject;
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
        Debug.Log("CreateBubble");
        Bubble bubble = Instantiate(bubblePrefab);
        bubble.SetPool(bubblePool, this);
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
