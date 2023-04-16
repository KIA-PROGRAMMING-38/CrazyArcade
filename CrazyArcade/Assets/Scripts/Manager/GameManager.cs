using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
        // TODO: �� ��ȯ ���� �Ŀ��� �������� ������ ��ȯ�ϴ� ��� �ҷ��;� ��
        MapManager.GetTotalMapInfo();
        ItemActions.SaveActions();
    }
}
