using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectManager : MonoBehaviour
{
    public static event Action<MapData> OnMapSelected;

    public MapData[] MapDataArr;
    public GameObject CurrentInfo;
    public GameObject SelectedInfo;
    private Button[] _mapSelectionButtons;
    private int _numOfMaps;
    public int _selectedMapNumber { get; set; } = 1;

    private void Awake()
    {
        _numOfMaps = transform.childCount;
        _mapSelectionButtons = new Button[_numOfMaps + 1];

        for(int i = 1; i <= _numOfMaps; ++i)
        {
            _mapSelectionButtons[i] = transform.GetChild(i - 1).GetComponent<Button>();
        }

        _mapSelectionButtons[_selectedMapNumber].GetComponent<Image>().color = _alphaMax;
        _mapSelectionButtons[_selectedMapNumber].transform.GetChild(1).GetComponent<Text>().color = _selectedNameColor;
        _mapSelectionButtons[_selectedMapNumber].transform.GetChild(2).GetComponent<Text>().color = _selectedNameColor;

        SetCurrentInfo(_selectedMapNumber);
        SetSelectedMap();

        transform.parent.gameObject.SetActive(false);
    }

    private Color _alphaZero = new Color(1f, 1f, 1f, 0f);
    private Color _alphaMax = new Color(1f, 1f, 1f, 1f);
    private Color _selectedNameColor = new Color(1f, 0.7474625f, 0f);
    private Color _unselectedNameColor = new Color(0.04705883f, 0.8745099f, 0.9960785f);
    public void UpdateSelectedMap()
    {
        for(int i = 1; i <= _numOfMaps; ++i)
        {
            _mapSelectionButtons[i].GetComponent<Image>().color = _alphaZero;
            _mapSelectionButtons[i].transform.GetChild(1).GetComponent<Text>().color = _unselectedNameColor;
            _mapSelectionButtons[i].transform.GetChild(2).GetComponent<Text>().color = _unselectedNameColor;

        }

        _mapSelectionButtons[_selectedMapNumber].GetComponent<Image>().color = _alphaMax;
        _mapSelectionButtons[_selectedMapNumber].transform.GetChild(1).GetComponent<Text>().color = _selectedNameColor;
        _mapSelectionButtons[_selectedMapNumber].transform.GetChild(2).GetComponent<Text>().color = _selectedNameColor;

        SetCurrentInfo(_selectedMapNumber);
    }

    private void SetCurrentInfo(int stageNum)
    {
        CurrentInfo.GetComponentInChildren<Text>().text = MapDataArr[stageNum].MaxPersonnel.ToString();
        CurrentInfo.GetComponentInChildren<Image>().sprite = MapDataArr[stageNum].PreviewImg;
    }

    public void SetSelectedMap()
    {
        OnMapSelected?.Invoke(MapDataArr[_selectedMapNumber]);
        GameManager.Instance.SelectedStage = MapDataArr[_selectedMapNumber];
        transform.parent.gameObject.SetActive(false);
    }
}
