using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionRoot : Explosion
{
    public override void SetPool(IObjectPool<Explosion> pool)
    {
        base.SetPool(pool);
    }
}
