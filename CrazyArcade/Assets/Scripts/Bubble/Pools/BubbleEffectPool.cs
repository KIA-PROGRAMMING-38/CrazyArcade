using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BubbleEffectPool : MonoBehaviour
{
    public BubbleEffect EffectPrefab;
    public IObjectPool<BubbleEffect> EffectPool;

    private void Awake()
    {
        EffectPool = new ObjectPool<BubbleEffect>
            (
                CreateEffect,
                OnGet,
                OnRelease,
                ActionOnDestroy,
                maxSize: 45
            );
    }

    private BubbleEffect CreateEffect()
    {
        BubbleEffect effect = Instantiate(EffectPrefab);
        effect.SetPool(EffectPool);
        return effect;
    }

    private void OnGet(BubbleEffect effect)
    {
        effect.gameObject.SetActive(true);
    }

    private void OnRelease(BubbleEffect effect)
    {
        effect.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(BubbleEffect effect)
    {
        Destroy(effect.gameObject);
    }
}
