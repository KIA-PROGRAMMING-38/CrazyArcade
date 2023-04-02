using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalAxisName;
    private string _verticalAxisName;

    private Vector2 _moveDirection;
    private float _speed = 1;
    private float _horizontal;
    private float _vertical;

    private Animator _animator;

    private void Awake()
    {
        _horizontalAxisName = name + "Horizontal";
        _verticalAxisName = name + "Vertical";

        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw(_horizontalAxisName);
        _vertical = Input.GetAxisRaw(_verticalAxisName);
        _moveDirection = new Vector2(_horizontal, _vertical);

        ActivateLayer(LayerName.WalkLayer);
        _animator.SetFloat("horizontal", _horizontal);
        _animator.SetFloat("vertical", _vertical);

        transform.Translate(_moveDirection * (_speed * Time.deltaTime));
    }

    public enum LayerName
    {
        BaseLayer = 0,
        WalkLayer = 1
    }

    private void ActivateLayer(LayerName layerName)
    {
        for (int i = 0; i < _animator.layerCount; ++i)
        {
            _animator.SetLayerWeight(i, 0);
        }

        _animator.SetLayerWeight((int)layerName, 1);
    }
}
