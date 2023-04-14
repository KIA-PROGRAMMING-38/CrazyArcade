using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDefault : StateMachineBehaviour
{
    private Block _block;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _block = animator.GetComponent<Block>();

        _block._canMove = true;
    }
}
