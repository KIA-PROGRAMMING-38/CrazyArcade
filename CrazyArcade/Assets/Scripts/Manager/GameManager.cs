using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public int SelectedStage;

    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
        // TODO: �� ��ȯ ���� �Ŀ��� �������� ������ ��ȯ�ϴ� ��� �ҷ��;� ��
        MapManager.GetTotalMapInfo();
        ItemActions.SaveActions();
        SelectedStage = 1;
    }
}
