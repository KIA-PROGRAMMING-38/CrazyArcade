using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayableCharacter _playableCharacter;
    private string _horizontalAxisName;
    private string _verticalAxisName;
    private string _putBubbleBtnName;

    private Vector2 _moveDirection;
    private float _horizontal;
    private float _vertical;
    public bool _isPutBubbleBtn { get; private set; }

    private Animator _animator;

    private void Awake()
    {
        _horizontalAxisName = name + "Horizontal";
        _verticalAxisName = name + "Vertical";
        _putBubbleBtnName = name + "Attack";
        Debug.Log(_putBubbleBtnName);

        _animator = transform.GetChild(0).GetComponent<Animator>();
        _playableCharacter = transform.GetChild(0).GetComponent<PlayableCharacter>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw(_horizontalAxisName);
        _vertical = Input.GetAxisRaw(_verticalAxisName);
        _isPutBubbleBtn = Input.GetButtonDown(_putBubbleBtnName);
        _moveDirection = new Vector2(_horizontal, _vertical);

        _animator.SetFloat("horizontal", _horizontal);
        _animator.SetFloat("vertical", _vertical);

        _playableCharacter.Move(_moveDirection);
    }
}
