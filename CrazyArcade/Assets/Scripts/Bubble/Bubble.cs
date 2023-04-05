using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble : MonoBehaviour
{
    private WaitForSeconds _boomReadyTime = new WaitForSeconds(3f);
    private Coroutine _boomReadyCoroutine;

    private void OnEnable()
    {
        _boomReadyCoroutine = StartCoroutine(BoomReady());
    }

    private IEnumerator BoomReady()
    {
        Debug.Log("BoomReady");

        yield return _boomReadyTime;

        Boom();
    }

    private void Boom()
    {
        Debug.Log("Boom »£√‚");
        bubblePool.Release(this);
    }

    private IObjectPool<Bubble> bubblePool;
    public void SetPool(IObjectPool<Bubble> pool)
    {
        bubblePool = pool;
    }
}
