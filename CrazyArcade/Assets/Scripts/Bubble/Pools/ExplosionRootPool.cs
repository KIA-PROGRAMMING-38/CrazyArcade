using UnityEngine;
using UnityEngine.Pool;

public class ExplosionRootPool : MonoBehaviour
{
    public Explosion effectPrefab;
    public IObjectPool<Explosion> rootPool;

    private void Awake()
    {
        rootPool = new ObjectPool<Explosion>
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
        effect.SetPool(rootPool);
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
