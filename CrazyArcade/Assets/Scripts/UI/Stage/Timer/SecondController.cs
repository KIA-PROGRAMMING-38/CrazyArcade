using UnityEngine;
using UnityEngine.UI;

public class SecondController : MonoBehaviour
{
    private Text _secondText;
    private int _second;

    private void Awake()
    {
        _secondText = GetComponent<Text>();
        _second = 00;
    }

    private void OnEnable()
    {
        TimerManager.OnSecondUpdate += UpdateSecond;
    }

    private void OnDisable()
    {
        TimerManager.OnSecondUpdate -= UpdateSecond;
    }

    private void UpdateSecond()
    {
        if(_second == 0)
        {
            _second = 59;
        }
        else
        {
            _second -= 1;
        }

        _secondText.text = NumLiteral.GetTimeNumString(_second);
    }
}
