using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;

public class FixedBlock : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
}
