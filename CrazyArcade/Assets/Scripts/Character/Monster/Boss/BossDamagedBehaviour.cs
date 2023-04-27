using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagedBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
