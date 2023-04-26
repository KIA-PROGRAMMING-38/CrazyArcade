using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInfoUI : MonoBehaviour
{
    private GameObject[] _playerSlots;
    private int _playersCount;

    private void Awake()
    {
        _playersCount = transform.childCount;
        _playerSlots = new GameObject[_playersCount];

        for(int i = 0; i < _playersCount; ++i)
        {
            _playerSlots[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        DrawPlayerInfoSlot();
    }

    private void DrawPlayerInfoSlot()
    {
        for(int i = 0; i < _playersCount; ++i)
        {
            GameObject player = _playerSlots[i];

            player.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("CharacterData/Stand/" + PlayersInfo.AllPlayersInfo[i].selectedCharacterId);
            player.GetComponentInChildren<Text>().text = PlayersInfo.AllPlayersInfo[i].name;
        }
    }
}
