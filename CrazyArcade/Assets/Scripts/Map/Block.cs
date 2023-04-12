using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static class MapAnimID
    {
        public static readonly int POP = Animator.StringToHash("Pop");
    }

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void Pop()
    {
        _animator.SetTrigger(MapAnimID.POP);
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
}
