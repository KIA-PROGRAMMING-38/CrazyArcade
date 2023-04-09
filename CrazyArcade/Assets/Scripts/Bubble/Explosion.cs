using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{
    public IObjectPool<Explosion> EffectPool;
    public virtual void SetPool(IObjectPool<Explosion> pool)
    {
        EffectPool = pool;
    }
}
