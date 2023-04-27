using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static event Action<Character> OnDie;
    public virtual void Move()
    {
        
    }

    public virtual void Attack()
    {

    }

    public virtual void Die()
    {
        OnDie?.Invoke(this);
    }
}
