using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{
    public event Action OnDisappear;
    public Explosion ParentNode;
    public IObjectPool<Explosion> EffectPool;

    public Animator Animator;

    public virtual void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void SetPool(IObjectPool<Explosion> pool)
    {
        EffectPool = pool;
    }

    public virtual void EventSubscribe()
    {
        if (ParentNode != null)
        {
            ParentNode.OnDisappear -= ReleaseReady;
            ParentNode.OnDisappear += ReleaseReady;
        }
    }

    public virtual void ReleaseReady()
    {
        Animator.SetTrigger("Disappear");
    }

    public virtual void Release()
    {
        EffectPool.Release(this);
    }

    public virtual void OnDisable()
    {
        OnDisappear?.Invoke();

        if (ParentNode != null)
        {
            ParentNode.OnDisappear -= ReleaseReady;
        }

        ParentNode = null;
    }
}
