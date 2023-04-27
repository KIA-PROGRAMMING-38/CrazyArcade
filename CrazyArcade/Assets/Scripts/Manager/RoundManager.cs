using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RESULT
{
    Win,
    Lose,
    Draw
}

public class RoundManager : MonoBehaviour
{
    public static event Action OnGameEnd;
    public List<Character> SurvivePlayersTeam1 = new List<Character>();
    public List<Character> SurvivePlayersTeam2 = new List<Character>();
    public Canvas ResultCanvas;

    private GAME_MODE _currentGameMode;
    private int _currentMapNumber;
    private GameObject _mapPrefab;

    private void Awake()
    {
        transform.SetParent(GameManager.Instance.transform);
        _currentGameMode = GameManager.Instance.SelectedStage.GameMode;
        _currentMapNumber = GameManager.Instance.SelectedStage.StageNumber;
        _mapPrefab = Resources.Load<GameObject>("Maps/Map" + _currentMapNumber);
        Instantiate(_mapPrefab);
    }

    private void Start()
    {
        AudioManager.Instance.PlaySFX("game_start");
        AudioManager.Instance.PlayBGM(GameManager.Instance.SelectedStage.name);
    }

    private void OnEnable()
    {
        PlayableCharacter.OnDie += CheckRoundEnded;
        TimerManager.OnTimeOver += JudgeWinner;
    }

    private void OnDisable()
    {
        PlayableCharacter.OnDie -= CheckRoundEnded;
        TimerManager.OnTimeOver -= JudgeWinner;
    }


    private int _team1Count;
    private int _team2Count;
    private void CheckRoundEnded(Character dieCharacter)
    {
        SurvivePlayersTeam1.Remove(dieCharacter);
        SurvivePlayersTeam2.Remove(dieCharacter);

        _team1Count = SurvivePlayersTeam1.Count;
        _team2Count = SurvivePlayersTeam2.Count;

        if(_team1Count == 0 || _team2Count == 0)
        {
            JudgeWinner();
        }
    }

    private void JudgeWinner()
    {
        switch(_currentGameMode)
        {
            case GAME_MODE.One_on_one:
                if(_team1Count > 0 && _team2Count == 0)
                {
                    PlayersInfo.SavePlayersResult(RESULT.Win, RESULT.Lose);
                }
                else if(_team2Count > 0 && _team1Count == 0)
                {
                    PlayersInfo.SavePlayersResult(RESULT.Lose, RESULT.Win);
                }
                else
                {
                    PlayersInfo.SavePlayersResult(RESULT.Draw, RESULT.Draw);
                }

                break;

            case GAME_MODE.Monster:
                if(_team1Count > 0 && _team2Count == 0)
                {
                    Debug.Log("Players win");
                    PlayersInfo.SavePlayersResult(RESULT.Win, RESULT.Win);
                }
                else if(_team1Count == 0)
                {
                    Debug.Log("Monster Win");
                    PlayersInfo.SavePlayersResult(RESULT.Lose, RESULT.Lose);
                }
                else
                {
                    Debug.Log("DRAW");
                    PlayersInfo.SavePlayersResult(RESULT.Draw, RESULT.Draw);
                }

                break;

            default:
                Debug.Assert(false, "Error in GameMode");
                break;
        }

        ResultCanvas.enabled = true;
        OnGameEnd?.Invoke();
        Invoke("StageEnd", 4f);
    }

    private const int LOBBY_SCENE_NUMBER = 0;
    private void StageEnd()
    {
        GameObject roundManager = GameManager.Instance.transform.GetChild(0).gameObject;
        Destroy(roundManager);
        SceneManager.LoadScene(LOBBY_SCENE_NUMBER);
        AudioManager.Instance.PlayBGM("lobby_bgm");
    }
}
