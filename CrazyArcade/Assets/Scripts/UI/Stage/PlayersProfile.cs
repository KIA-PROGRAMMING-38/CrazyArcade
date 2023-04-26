using UnityEngine;
using UnityEngine.UI;

public class PlayersProfile : MonoBehaviour
{
    private GameObject[] _profileSlots;
    private int _playersCount;

    private void Awake()
    {
        _playersCount = transform.childCount;
        _profileSlots = new GameObject[_playersCount];

        for (int i = 0; i < _playersCount; ++i)
        {
            _profileSlots[i] = transform.GetChild(i).gameObject;
        }
    }
    private void Start()
    {
        DrawPlayerProfileSlot();
    }

    private void DrawPlayerProfileSlot()
    {
        for (int i = 0; i < _playersCount; ++i)
        {
            GameObject player = _profileSlots[i];

            player.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("CharacterData/Portrait/" + PlayersInfo.AllPlayersInfo[i].selectedCharacterId);
            player.GetComponentInChildren<Text>().text = PlayersInfo.AllPlayersInfo[i].name;
        }
    }
}
