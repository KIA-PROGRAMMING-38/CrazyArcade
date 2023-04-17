using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            collision.gameObject.GetComponent<Bubble>().Boom();
        }
    }
}
