using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionEffect : Explosion
{
    private float _explosionInterval;
    private float _elapsedTime;
    
    public override void Start()
    {
        base.Start();
        _explosionInterval = GameManager.Instance.ExplosionInterval;
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _explosionInterval)
        {
            _elapsedTime = 0;
            Animator.SetTrigger("Advance");
        }
    }

    public override void ReleaseReady()
    {
        base.ReleaseReady();
    }

    public override void Release()
    {
        base.Release();
    }

    public override void SetPool(IObjectPool<Explosion> pool)
    {
        base.SetPool(pool);
    }

    public override void EventSubscribe()
    {
        base.EventSubscribe();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
}
