using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectManager : MonoBehaviour
{
    private Button[] _mapSelectionButtons;
    private int _numOfMaps;
    public int _selectedMapNumber { get; set; }

    private void Awake()
    {
        _numOfMaps = transform.childCount;
        _mapSelectionButtons = new Button[_numOfMaps + 1];

        for(int i = 1; i <= _numOfMaps; ++i)
        {
            _mapSelectionButtons[i] = transform.GetChild(i - 1).GetComponent<Button>();
        }
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
            _mapSelectionButtons[i].GetComponentInChildren<Text>().color = _unselectedNameColor;
        }

        _mapSelectionButtons[_selectedMapNumber].GetComponent<Image>().color = _alphaMax;
        _mapSelectionButtons[_selectedMapNumber].GetComponentInChildren<Text>().color = _selectedNameColor;
    }

    public void SetSelectedMap()
    {
        GameManager.Instance.SelectedStage = _selectedMapNumber;
        transform.parent.gameObject.SetActive(false);
    }
}
