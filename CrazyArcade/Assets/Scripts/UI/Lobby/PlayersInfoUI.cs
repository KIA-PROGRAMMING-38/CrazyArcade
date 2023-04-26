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
            Sprite[] sprites = Resources.LoadAll<Sprite>("CharacterData/Stand/" + "bazzi");

            player.GetComponentInChildren<Image>().sprite = sprites[5];
            player.GetComponentInChildren<Text>().text = PlayersInfo.AllPlayersInfo[i].name;
        }
    }
}
