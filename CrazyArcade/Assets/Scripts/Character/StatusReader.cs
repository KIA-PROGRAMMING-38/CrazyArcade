using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusReader : MonoBehaviour
{
    private TextAsset textAssetData;

    [System.Serializable]
    public struct PlayableCharacterStatus
    {
        public int id;
        public string name;
        public int count;
        public int maxCount;
        public int power;
        public int maxPower;
        public int speed;
        public int maxSpeed;
    }

    public PlayableCharacterStatus[] PlayableCharacters;

    void Awake()
    {
        textAssetData = Resources.Load<TextAsset>("CharacterData/Status");
        Debug.Assert(textAssetData != null, "PlayableCharacter Status 파일을 불러오지 못했습니다.");

        ReadCSVData();
    }

    void ReadCSVData()
    {
        string[] data = textAssetData.text.Split(new char[] {',', '\n'});

        int tableSize = data.Length / 9 - 1;  // 가장 윗 행은 데이터가 아니므로 -1
        PlayableCharacters = new PlayableCharacterStatus[tableSize];

        for (int i = 0; i < tableSize; ++i)
        {
            PlayableCharacters[i] = new PlayableCharacterStatus();
            PlayableCharacters[i].id = int.Parse(data[9 * (i + 1)]);
            PlayableCharacters[i].name = data[9 * (i + 1) + 2];
            PlayableCharacters[i].count = int.Parse(data[9 * (i + 1) + 3]);
            PlayableCharacters[i].maxCount = int.Parse(data[9 * (i + 1) + 4]);
            PlayableCharacters[i].power = int.Parse(data[9 * (i + 1) + 5]);
            PlayableCharacters[i].maxPower = int.Parse(data[9 * (i + 1) + 6]);
            PlayableCharacters[i].speed = int.Parse(data[9 * (i + 1) + 7]);
            PlayableCharacters[i].maxSpeed = int.Parse(data[9 * (i + 1) + 8]);
        }
    }
}
