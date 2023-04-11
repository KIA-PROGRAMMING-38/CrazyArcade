using UnityEngine;
using UnityEngine.Pool;

public class BubbleEffect : MonoBehaviour
{
    public enum EffectType
    {
        Root,
        Link,
        Edge
    }

    public enum EffectDirection
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    private EffectType Type;
    private EffectDirection Direction;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayableCharacter>().ReadyDie();
        }
    }

    public void SetEffectInfo(int type, int direction)
    {
        Type = (EffectType)type;
        Direction = (EffectDirection)direction;
        _animator.SetFloat("Type", (float)Type);
        _animator.SetFloat("Direction", (float)Direction);
    }

    private IObjectPool<BubbleEffect> EffectPool;
    public void SetPool(IObjectPool<BubbleEffect> pool)
    {
        EffectPool = pool;
    }

    public void DisappearReady()
    {
        _animator.SetTrigger("Dissapear");
    }

    public void Disappear()
    {
        EffectPool.Release(this);
    }
}
