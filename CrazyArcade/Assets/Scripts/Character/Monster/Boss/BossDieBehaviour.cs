using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieBehaviour : StateMachineBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _elapsedTime;
    private float _changeTerm = 0.2f;
    private Color _darkColor;
    private Color _lightColor;
    private int _changeCount;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
        _lightColor = _spriteRenderer.material.color;
        _darkColor = new Color(0.8f, 0.8f, 0.8f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _changeTerm)
        {
            _elapsedTime = 0;
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        Color curColor = _spriteRenderer.material.color;

        if(curColor == _darkColor)
        {
            _spriteRenderer.material.color = _lightColor;
        }
        else
        {
            _spriteRenderer.material.color = _darkColor;
        }

        ++_changeCount;
    }
}
