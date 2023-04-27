using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomDialog : MonoBehaviour
{
    public string[] dialog;
    float elapsedTime;
    float changeInterval;
    Camera mainCam;
    [SerializeField] Transform boss;
    TextMeshProUGUI _tmp;

    private void Awake()
    {
        mainCam = Camera.main;
        changeInterval = 3f;
       _tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        // position
        Vector3 pos = boss.position;
        pos += Vector3.up * 2f;
        transform.position = mainCam.WorldToScreenPoint(pos);

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= changeInterval) 
        {
            elapsedTime = 0;
            int rand = Random.Range(0, dialog.Length);
            _tmp.text = dialog[rand];
        }
    }
}
