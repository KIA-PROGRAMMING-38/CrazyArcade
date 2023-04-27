using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            collision.GetComponent<PlayableCharacter>().ImmediatelyDie();
        }
    }
}
