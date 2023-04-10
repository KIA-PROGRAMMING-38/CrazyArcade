using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{
    public event Action OnDisappear;
    public Explosion ParentNode;
    public IObjectPool<Explosion> EffectPool;
    public virtual void SetPool(IObjectPool<Explosion> pool)
    {
        EffectPool = pool;
    }

    public virtual void EventSubscribe()
    {
        if (ParentNode != null)
        {
            ParentNode.OnDisappear -= HelpCoroutine;
            ParentNode.OnDisappear += HelpCoroutine;
        }
    }

    public void HelpCoroutine()
    {
        StartCoroutine(ReleaseReady());
    }    

    public virtual IEnumerator ReleaseReady()
    {
        // TODO: 코루틴 없애고 애니메이션 재생 후 Release 구문있는 메소드 호출할 수 있도록 변경
        yield return new WaitForSeconds(1f);
        EffectPool.Release(this);
    }

    public virtual void OnDisable()
    {
        OnDisappear?.Invoke();

        if (ParentNode != null)
        {
            ParentNode.OnDisappear -= HelpCoroutine;
        }

        ParentNode = null;
    }
}
