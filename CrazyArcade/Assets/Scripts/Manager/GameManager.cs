using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBehaviour<GameManager>
{
    public float ExplosionInterval = 0.01f;
    protected override void Awake()
    {
        base.Awake();
        DataReader.ReadData();
    }
}
