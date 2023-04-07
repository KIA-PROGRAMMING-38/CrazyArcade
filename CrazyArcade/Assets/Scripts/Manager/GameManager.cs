using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    private void Awake()
    {
        DataReader.ReadData();
    }
}
