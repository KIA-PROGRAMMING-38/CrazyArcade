using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble : MonoBehaviour
{
    private float _boomReadyTime = 3f;
    private float _elapsedTime;

    private void OnEnable()
    {
        _elapsedTime = 0f;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _boomReadyTime)
        {
            Boom();
        }
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
