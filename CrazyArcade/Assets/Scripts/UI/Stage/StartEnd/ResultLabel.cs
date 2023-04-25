using UnityEngine;
using UnityEngine.UI;

public class ResultLabel : MonoBehaviour
{
    public Sprite[] LabelSprites;
    private Image _label;
    private GAME_MODE _currentGameMode;
    private Vector2 _logoSize;

    private void Awake()
    {
        _label = GetComponent<Image>();
        _currentGameMode = GameManager.Instance.SelectedStage.GameMode;

        _logoSize = new Vector2(200, 56);
    }

    private void OnEnable()
    {
        RoundManager.OnGameEnd += DecideLabelImage;
    }

    private void OnDisable()
    {
        RoundManager.OnGameEnd -= DecideLabelImage;
    }

    public void DecideLabelImage()
    {
        switch(_currentGameMode)
        {
            case GAME_MODE.One_on_one:
                if(PlayersInfo.Player1Info.result == RESULT.Win)
                {
                    _label.sprite = LabelSprites[0];
                }
                else if(PlayersInfo.Player2Info.result == RESULT.Win)
                {
                    _label.sprite = LabelSprites[1];
                }
                else if(PlayersInfo.Player1Info.result == RESULT.Draw)
                {
                    _label.rectTransform.sizeDelta = _logoSize;
                    _label.sprite = LabelSprites[3];
                }
                break;

            case GAME_MODE.Monster:
                _label.rectTransform.sizeDelta = _logoSize;

                if (PlayersInfo.Player1Info.result == RESULT.Win)
                {
                    _label.sprite = LabelSprites[2];
                }
                else if(PlayersInfo.Player1Info.result == RESULT.Draw)
                {
                    _label.sprite = LabelSprites[3];
                }
                else if(PlayersInfo.Player1Info.result == RESULT.Lose)
                {
                    _label.sprite = LabelSprites[4];
                }
                break;
        }
    }
}