using System.Collections;
using UnityEngine;

public class StartWord : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _fadeTime;
    private float _elapsedTime;

    void Start()
    {
        _startPosition = transform.position;
        _endPosition = transform.position;
        _endPosition.y += 400;
    }

    public void SetFadeTime(float time)
    {
        _fadeTime = time;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        while(_elapsedTime < _fadeTime)
        {
            _elapsedTime += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(_startPosition, _endPosition, _elapsedTime / _fadeTime);
            transform.position = newPos;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
