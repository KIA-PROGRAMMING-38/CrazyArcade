using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionButton : MonoBehaviour
{
    public MapData mapData;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetSelectedMapData()
    {
        GameManager.Instance.SelectedStage = mapData.StageNumber;
    }
}
