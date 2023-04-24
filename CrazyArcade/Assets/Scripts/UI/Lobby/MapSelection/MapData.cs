using UnityEngine;

public enum GAME_MODE
{
    One_on_one,
    Monster
}

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public int StageNumber;
    public string StageName;
    public int MaxPersonnel;
    public Sprite PreviewImg;
    public GAME_MODE GameMode;
}
