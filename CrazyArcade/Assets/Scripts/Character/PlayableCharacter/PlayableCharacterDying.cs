using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterDying : StateMachineBehaviour
{
    PlayableCharacter _playableCharacter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playableCharacter = animator.GetComponent<PlayableCharacter>();
        _playableCharacter._savedSpeed = _playableCharacter._speed;
        _playableCharacter._speed = 0.2f;
    }
}
