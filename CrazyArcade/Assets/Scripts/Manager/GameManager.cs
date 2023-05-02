using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public MapData SelectedStage;

    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
        MapManager.GetTotalMapInfo();
        ItemActions.SaveActions();

        PlayersInfo.SavePlayersName("�÷��̾� 1", "�÷��̾� 2");
        PlayersInfo.SavePlayerCharacter("0", "0");
    }

    private void Start()
    {
        SetResolution();
    }

    public void SetResolution()
    {
        int setWidth = 800;
        int setHeight = 600;

        Screen.SetResolution(setWidth, setHeight, true);
    }
}
