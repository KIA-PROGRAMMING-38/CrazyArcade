using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDyingBehaviour : StateMachineBehaviour
{
    private float _elapsedTime;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= 5f)
        {
            animator.SetTrigger(BossMonster.BossAnimID.DIE);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime = 0f;
    }
}
