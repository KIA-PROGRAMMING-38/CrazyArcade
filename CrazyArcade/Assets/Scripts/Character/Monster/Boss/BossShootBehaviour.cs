using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBehaviour : StateMachineBehaviour
{
    private int _loopCount;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1.0f)
        {
            ++_loopCount;
            if(_loopCount == 3)
            {
                _loopCount = 0;
                animator.GetComponent<BossMonster>().Shoot();
                animator.SetTrigger(BossMonster.BossAnimID.SET_IDLE);
            }
            else
            {
                animator.Play(stateInfo.fullPathHash, layerIndex, 0f);
            }
        }
    }
}
