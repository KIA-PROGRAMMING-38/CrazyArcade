using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionRoot : Explosion
{
    private float _elapsedTime;
    public override void SetPool(IObjectPool<Explosion> pool)
    {
        base.SetPool(pool);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= 1f)
        {
            _elapsedTime = 0f;
            EffectPool.Release(this);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
}
