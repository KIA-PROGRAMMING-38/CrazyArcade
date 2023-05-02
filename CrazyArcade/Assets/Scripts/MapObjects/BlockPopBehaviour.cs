using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPopBehaviour : StateMachineBehaviour
{
    // TODO: �������� ȿ�� update����
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Collider2D>().enabled = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Collider2D>().enabled = true;
        animator.gameObject.SetActive(false);
    }
}
