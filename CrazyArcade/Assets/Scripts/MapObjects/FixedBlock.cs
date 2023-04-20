using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;

public class FixedBlock : MonoBehaviour
{
    public static event Action<Transform> OnFixedBlockBreak;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BubbleEffect"))
        {
            _animator.SetTrigger(MapAnimID.POP);
        }
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        _spriteRenderer.enabled = false;
    }

    public void Enable()
    {
        _spriteRenderer.enabled = true;
    }

    private void GetItem()
    {
        OnFixedBlockBreak?.Invoke(transform);
    }
}
