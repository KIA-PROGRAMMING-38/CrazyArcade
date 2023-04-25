using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static event Action OnMinuteUpdate;
    public static event Action OnSecondUpdate;

    private int _currentMinute;
    private int _currentSecond;
    private float _elapsedTime;
    private bool _firstCount = true;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= 1f)
        {
            CountTime();
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// 1초가 지났을 때 호출
    /// </summary>
    private void CountTime()
    {
        _currentSecond += 1;
        OnSecondUpdate?.Invoke();

        if(_firstCount)
        {
            OnMinuteUpdate?.Invoke();
            _firstCount = false;
        }


        if(_currentSecond == 60)
        {
            _currentSecond = 0;
            _currentMinute += 1;
            
            if(_currentMinute == 1)
            {
                TimeOver();
            }

            OnMinuteUpdate?.Invoke();
        }

    }

    private void TimeOver()
    {
        // TODO: 승패 판정 메소드 호출
        this.enabled = false;
    }
}
