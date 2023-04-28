using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehaviour : StateMachineBehaviour
{
    float _elapsedTime;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= 3f)
        {
            // ���� Ÿ���� �޾ƿͼ� �� �� �ϳ��� ����
            animator.GetComponent<BossMonster>().DecideNextBehaviour();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime = 0f;
    }
}
