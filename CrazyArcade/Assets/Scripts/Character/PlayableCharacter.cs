using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : Character
{
    private float _speed = 1f;
    private float deltaTime;

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        deltaTime = Time.deltaTime;
        transform.Translate(direction * (_speed * deltaTime));
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log($"{name}: π∞«≥º± ≥ı¿Ω");
    }

    public override void Die()
    {
        base.Die();
    }
}
