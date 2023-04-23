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
        // TODO: �� ��ȯ ���� �Ŀ��� �������� ������ ��ȯ�ϴ� ��� �ҷ��;� ��
        MapManager.GetTotalMapInfo();
        ItemActions.SaveActions();

        PlayersInfo.SavePlayersName("�÷��̾� 1", "�÷��̾� 2");
    }
}
