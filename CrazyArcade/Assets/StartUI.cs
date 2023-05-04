using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private const float STANDBY_TIME = 1f;
    [SerializeField] private const float WORD_FADEOUT_TIME = 0.1f;
    [SerializeField] private const float WORD_FADEOUT_INTERVAL = 0.1f;
    [SerializeField] private GameObject _panel;
    private StartWord[] _words;

    void Start()
    {
        _panel.SetActive(false);
        _words = GetComponentsInChildren<StartWord>();
        StartCoroutine(StartUIFadeOut());
        for(int i = 0; i < _words.Length; i++)
        {
            _words[i].SetFadeTime(WORD_FADEOUT_TIME);
        }
    }

    private IEnumerator StartUIFadeOut()
    {
        WaitForSeconds fadeoutInterval = new WaitForSeconds(WORD_FADEOUT_INTERVAL);
        WaitForSeconds standbyTime = new WaitForSeconds(STANDBY_TIME);

        for (int i = 0; i < _words.Length; i++)
        {
            _words[i].gameObject.SetActive(true);
        }
        _panel.SetActive(true);
        yield return standbyTime;

        for (int i = 0; i < _words.Length; i++)
        {
            _words[i].FadeOut();

            if(i != _words.Length - 1)
            {
                yield return fadeoutInterval;
            }
            else
            {
                yield return standbyTime;
            }
        }

        _panel.SetActive(false);
    }
}
