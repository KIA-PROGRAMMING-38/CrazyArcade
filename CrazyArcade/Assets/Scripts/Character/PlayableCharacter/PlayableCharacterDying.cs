using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayableCharacter;

public class PlayableCharacterDying : StateMachineBehaviour
{
    private Status _status;
    private SpriteRenderer _spriteRenderer;
    private Color _color;

    private float _elapsedTime;
    private float _duration;
    private float _startAlpha;
    private float _lastAlpha;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.Instance.PlaySFX("dying");
        // 속도 변화
        _status = animator.GetComponent<Status>();
        _status.SpeedDebuff = true;

        // alpha값 변화
        _spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
        _startAlpha = 0.7f;
        _lastAlpha = 1f;
        _duration = 5f;

        _color = _spriteRenderer.material.color;
        _color.a = _startAlpha;
        _spriteRenderer.material.color = _color;

        animator.gameObject.layer = LayerMask.NameToLayer("DyingPlayer");
        animator.GetComponent<PlayableCharacter>().IsAlive = false;
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

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _color.a = 1f;
        _spriteRenderer.material.color = _color;
        _status.MoveRestrict = true;

        animator.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
