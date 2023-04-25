using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumLiteral
{
    private static readonly Dictionary<int, string> _container = new();
    public static string GetNumString(int num)
    {
        if(false == _container.ContainsKey(num))
        {
            _container.Add(num, $"{num}");
        }

        return _container[num];
    }

    private static readonly string[] _timerContainer = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09" };
    /// <summary>
    /// 인자로 넘긴 정수의 문자열을 반환
    /// </summary>
    public static string GetTimeNumString(int num)
    {
        if(num < 10)
        {
            return _timerContainer[num];
        }

        return GetNumString(num);
    }
}
