using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public virtual void Move(Vector2 direction)
    {
        
    }

    public virtual void Attack()
    {

    }

    public virtual void Die()
    {
        Debug.Log($"{name}: Die »£√‚");
    }
}
