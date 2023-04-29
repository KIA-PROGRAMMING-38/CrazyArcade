using UnityEngine;
using UnityEngine.UI;

public class MinuteController : MonoBehaviour
{
    private Text _minuteText;
    private int _minute;

    private void Awake()
    {
        _minuteText = GetComponent<Text>();
        _minute = 1;
    }

    private void OnEnable()
    {
        TimerManager.OnMinuteUpdate += UpdateMinute;
    }

    private void OnDisable()
    {
        TimerManager.OnMinuteUpdate -= UpdateMinute;
    }

    private void UpdateMinute()
    {
        _minute -= 1;
        _minuteText.text = NumLiteral.GetTimeNumString(_minute);
    }
}
