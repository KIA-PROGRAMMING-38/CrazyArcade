using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterRevival : StateMachineBehaviour
{
    Status _status;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = false;
        _status = animator.GetComponent<Status>();
        _status.MoveRestrict = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = true;
        _status.MoveRestrict = false;
        _status.SpeedDebuff = false;
        animator.GetComponent<PlayableCharacter>().IsAlive = true;
    }
}
