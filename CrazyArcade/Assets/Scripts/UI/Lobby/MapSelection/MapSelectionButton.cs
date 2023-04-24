using UnityEngine;
using UnityEngine.UI;

// RoundManager -> ������ ������ �� -> ��� �������� ��� UI Presenter �� �����ְ�
// ��� UI���� �� �������� �������� ����
public class MapSelectionButton : MonoBehaviour
{
    public MapData mapData;
    private Image _buttonImage;
    private Text _maxPersonnel;
    private MapSelectManager _manager;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _buttonImage.alphaHitTestMinimumThreshold = 0.1f;
        _manager = transform.parent.GetComponent<MapSelectManager>();
        _maxPersonnel = transform.GetChild(2).GetComponent<Text>();
        _maxPersonnel.text = mapData.MaxPersonnel.ToString();
    }

    public void SetSelectedMapData()
    {
        _manager._selectedMapNumber = mapData.StageNumber;
        _manager.UpdateSelectedMap();
    }
}
