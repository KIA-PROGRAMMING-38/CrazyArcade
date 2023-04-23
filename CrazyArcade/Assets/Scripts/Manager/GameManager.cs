using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public MapData SelectedStage;
    public int winner;

    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
        // TODO: 씬 전환 구현 후에는 스테이지 씬으로 전환하는 경우 불러와야 함
        MapManager.GetTotalMapInfo();
        ItemActions.SaveActions();

        PlayersInfo.SavePlayersName("플레이어 1", "플레이어 2");
    }
}
