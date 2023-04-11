using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public float ExplosionInterval = 0.005f;
    public MapManager MapManager;
    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
        MapManager = new MapManager();
    }
}
