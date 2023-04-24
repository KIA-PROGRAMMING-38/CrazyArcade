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

    private void Awake()
    {
        transform.SetParent(GameManager.Instance.transform);
        _currentGameMode = GameManager.Instance.SelectedStage.GameMode;
    }

    private void OnEnable()
    {
        PlayableCharacter.OnDie += CheckRoundEnded;
    }

    private void OnDisable()
    {
        PlayableCharacter.OnDie -= CheckRoundEnded;
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
                    Debug.Log("player1 win");
                    PlayersInfo.SavePlayersResult(RESULT.Win, RESULT.Lose);
                }
                else if(_team2Count > 0 && _team1Count == 0)
                {
                    Debug.Log("player2 win");
                    PlayersInfo.SavePlayersResult(RESULT.Lose, RESULT.Win);
                }
                else
                {
                    Debug.Log("DRAW");
                    PlayersInfo.SavePlayersResult(RESULT.Draw, RESULT.Draw);
                }

                break;

            case GAME_MODE.Monster:
                if(_team1Count > 0)
                {
                    Debug.Log("Players win");
                }

                break;

            default:
                Debug.Assert(false, "Error in GameMode");
                break;
        }

        ResultCanvas.enabled = true;
        OnGameEnd?.Invoke();
        Invoke("StageEnd", 2f);
    }

    private const int LOBBY_SCENE_NUMBER = 0;
    private void StageEnd()
    {
        GameObject roundManager = GameManager.Instance.transform.GetChild(0).gameObject;
        Destroy(roundManager);
        SceneManager.LoadScene(LOBBY_SCENE_NUMBER);
    }
}
