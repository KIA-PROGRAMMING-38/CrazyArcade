using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionButton : MonoBehaviour
{
    public MapData mapData;
    private Button _button;
    private Image _buttonImage;
    private MapSelectManager _manager;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();
        _buttonImage.alphaHitTestMinimumThreshold = 0.1f;
        _manager = transform.parent.GetComponent<MapSelectManager>();
    }

    public void SetSelectedMapData()
    {
        _manager._selectedMapNumber = mapData.StageNumber;
        _manager.UpdateSelectedMap();
    }
}
