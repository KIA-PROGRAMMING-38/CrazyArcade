using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public float ExplosionInterval = 0.1f;
    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0.1f;
        DataReader.ReadData();
    }
}
