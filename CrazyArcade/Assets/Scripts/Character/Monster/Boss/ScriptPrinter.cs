using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

class ScriptPrinter : MonoBehaviour
{
    [SerializeField]
    private GameObject _textObject;
    private TextMeshProUGUI _text;
    private Camera _mainCamera;
    [SerializeField] private BossMonster _boss;

    private string _currentScript;

    private bool _isActive;
    private static readonly float _activeTime = 1f;
    private float _activeElapsedTime;

    void Awake()
    {
        _mainCamera = Camera.main;
        _text = _textObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        BossMonster.OnSpeak += PrintRandomScripts;
    }

    private void OnDisable()
    {
        BossMonster.OnSpeak -= PrintRandomScripts;
    }


    private void Update()
    {
        Vector3 pos = _boss.transform.position + (Vector3.up * 2.5f);
        pos = _mainCamera.WorldToScreenPoint(pos);
        transform.position = pos;
        if(_isActive)
        {
            _activeElapsedTime += Time.deltaTime;
            if(_activeElapsedTime > _activeTime)
            {
                _activeElapsedTime = 0f;
                _isActive = false;
                _textObject.SetActive(false);
            }
        }
    }

    void PrintRandomScripts(string[] scripts)
    {
        int rand = Random.Range(0, scripts.Length);
        _currentScript = scripts[rand];
        _isActive = true;
        _activeElapsedTime = 0f;
        _text.text = _currentScript;
        _textObject.SetActive(true);
    }

    public void PrintScript(string message)
    {
        _currentScript = message;
        _text.text = message;
        _isActive = true;
        _activeElapsedTime = 0f;
        _textObject.SetActive(true);
    }
}