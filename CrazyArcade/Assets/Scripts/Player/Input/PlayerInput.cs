using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string _horizontalAxisName;
    private string _verticalAxisName;
    private string _putBubbleBtnName;
    private string _useItemBtnName;

    public float _horizontal { get; set; }
    public float _vertical { get; set; }
    public bool _isPutBubbleBtn { get; set; }
    public bool _isUseItemBtn { get; set; }

    private void Awake()
    {
        _horizontalAxisName = name + "Horizontal";
        _verticalAxisName = name + "Vertical";
        _putBubbleBtnName = name + "Attack";
        _useItemBtnName = name + "UseItem";
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw(_horizontalAxisName);
        _vertical = Input.GetAxisRaw(_verticalAxisName);
        _isPutBubbleBtn = Input.GetButtonDown(_putBubbleBtnName);
        _isUseItemBtn = Input.GetButtonDown(_useItemBtnName);
    }
}
