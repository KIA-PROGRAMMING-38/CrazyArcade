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
        // TODO: �ڷ�ƾ ���ְ� �ִϸ��̼� ��� �� Release �����ִ� �޼ҵ� ȣ���� �� �ֵ��� ����
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
