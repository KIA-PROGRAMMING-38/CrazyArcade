using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalAxisName;
    private string _verticalAxisName;
    private string _putBubbleBtnName;

    public float _horizontal { get; private set; }
    public float _vertical { get; private set; }
    public bool _isPutBubbleBtn { get; private set; }

    private void Awake()
    {
        _horizontalAxisName = name + "Horizontal";
        _verticalAxisName = name + "Vertical";
        _putBubbleBtnName = name + "Attack";
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw(_horizontalAxisName);
        _vertical = Input.GetAxisRaw(_verticalAxisName);
        _isPutBubbleBtn = Input.GetButtonDown(_putBubbleBtnName);
    }
}
