using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayableCharacter;

public class PlayableCharacterDying : StateMachineBehaviour
{
    private PlayableCharacter _playableCharacter;
    private SpriteRenderer _spriteRenderer;
    private Color _color;

    private float _deltaTime;
    private float _elapsedTime;
    private float _duration;
    private float _startAlpha;
    private float _lastAlpha;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 속도 변화
        _playableCharacter = animator.GetComponent<PlayableCharacter>();
        _playableCharacter._savedSpeed = _playableCharacter._speed;
        _playableCharacter._speed = 0.2f;

        // alpha값 변화
        _spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
        _startAlpha = 0.7f;
        _lastAlpha = 1f;
        _duration = 5f;

        _color = _spriteRenderer.material.color;
        _color.a = _startAlpha;
        _spriteRenderer.material.color = _color;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _duration)
        {
            animator.SetTrigger(PlayerAnimID.IS_DYING_LAST);
        }

        _color.a = Mathf.Lerp(_startAlpha, _lastAlpha, _elapsedTime / _duration);
        _spriteRenderer.material.color = _color;
    }

}
