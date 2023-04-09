using UnityEngine;
using UnityEngine.Pool;

public class ExplosionEndDownPool : MonoBehaviour
{
    public Explosion effectPrefab;
    public IObjectPool<Explosion> effectPool;

    private void Awake()
    {
        effectPool = new ObjectPool<Explosion>
            (
                CreateEffect,
                OnGet,
                OnRelease,
                ActionOnDestroy,
                maxSize: 10
            );
    }

    private Explosion CreateEffect()
    {
        Explosion effect = Instantiate(effectPrefab);
        effect.SetPool(effectPool);
        return effect;
    }

    private void OnGet(Explosion effect)
    {
        effect.gameObject.SetActive(true);
    }

    private void OnRelease(Explosion effect)
    {
        effect.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Explosion effect)
    {
        Destroy(effect.gameObject);
    }
}
