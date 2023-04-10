using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionEffect : Explosion
{
    private Animator _animator;
    private float _explosionInterval;
    private float _elapsedTime;
    void Start()
    {
        _explosionInterval = GameManager.Instance.ExplosionInterval;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _explosionInterval)
        {
            _elapsedTime = 0;
            _animator.SetTrigger("Advance");
        }
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
