using UnityEngine;
using UnityEngine.Pool;

public class BubblePool
{
    public Bubble bubblePrefab;
    // public GameObject Player;
    private PlayableCharacter _player;
    public IObjectPool<Bubble> bubblePool;

    public BubblePool(PlayableCharacter player)
    {
        SetPlayer(player);
        bubblePrefab = Resources.Load<Bubble>("Bubble");
        bubblePool = new ObjectPool<Bubble>
            (
                CreateBubble,
                OnGet,
                OnRelease,
                ActionOnDestroy,
                maxSize: 10
            );
    }

    public void SetPlayer(PlayableCharacter player ) => _player = player;


    private Bubble CreateBubble()
    {
        Bubble bubble = MonoBehaviour.Instantiate(bubblePrefab);
        bubble.SetPool(bubblePool);
        return bubble;
    }

    private void OnGet(Bubble bubble)
    {
        bubble.gameObject.SetActive(true);
        bubble.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnRelease(Bubble bubble)
    {
        bubble.gameObject.SetActive(false);
        _player.DecreaseCurrentCount();
    }

    private void ActionOnDestroy(Bubble bubble)
    {
        MonoBehaviour.Destroy(bubble.gameObject);
    }
}
