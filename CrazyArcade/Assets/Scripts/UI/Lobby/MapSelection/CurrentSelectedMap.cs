using UnityEngine;
using UnityEngine.UI;

public class CurrentSelectedMap : MonoBehaviour
{
    private Text _name;
    private Text _personnel;
    private Image _previewImg;

    private void Awake()
    {
        _name = transform.GetChild(0).GetComponent<Text>();
        _personnel = transform.GetChild(1).GetComponent<Text>();
        _previewImg = transform.GetChild(2).GetComponent<Image>();
    }

    private void OnEnable()
    {
        MapSelectManager.OnMapSelected += ShowSelectedMapInfo;
    }

    private void OnDisable()
    {
        MapSelectManager.OnMapSelected -= ShowSelectedMapInfo;
    }

    private void ShowSelectedMapInfo(MapData selectedMapData)
    {
        _name.text = selectedMapData.StageName;
        _personnel.text = selectedMapData.MaxPersonnel.ToString();
        _previewImg.sprite = selectedMapData.PreviewImg;
    }
}
