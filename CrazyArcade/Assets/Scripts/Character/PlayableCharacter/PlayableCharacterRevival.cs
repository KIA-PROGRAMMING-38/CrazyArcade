using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterRevival : StateMachineBehaviour
{
    PlayableCharacter _playableCharacter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = false;
        _playableCharacter = animator.GetComponent<PlayableCharacter>();
        _playableCharacter._speed = 0f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = true;
        _playableCharacter._speed = _playableCharacter._savedSpeed;
    }
}
