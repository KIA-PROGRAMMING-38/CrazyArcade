using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Bubble Idle Enter");
    }
}
