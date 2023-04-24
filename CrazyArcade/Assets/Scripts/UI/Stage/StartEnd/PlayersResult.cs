using UnityEngine;
using UnityEngine.UI;

public class PlayersResult : MonoBehaviour
{
    public Sprite[] ResultSprites;
    public GameObject[] _playerResults;
    private int _childCount;

    private void Awake()
    {
        _childCount = transform.childCount;
        _playerResults = new GameObject[_childCount];

        for(int i = 0; i < _childCount; ++i)
        {
            _playerResults[i] = transform.GetChild(i).gameObject;
        }

        transform.root.GetComponent<Canvas>().enabled = false;
    }

    private void OnEnable()
    {
        RoundManager.OnGameEnd += SetPlayersResult;
    }

    private void OnDisable()
    {
        RoundManager.OnGameEnd -= SetPlayersResult;
    }

    private void SetPlayersResult()
    {
        for (int i = 0; i < _childCount; ++i)
        {
            GameObject playerResult = _playerResults[i];

            playerResult.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
            playerResult.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = GetResultSprite(i);
            playerResult.transform.GetChild(2).gameObject.GetComponent<Text>().text = PlayersInfo.AllPlayersInfo[i].name;
        }
    }

    private Sprite GetResultSprite(int playerIndex)
    {
        RESULT result = PlayersInfo.AllPlayersInfo[playerIndex].result;

        switch(result)
        {
            case RESULT.Win:
                return ResultSprites[0];

            case RESULT.Draw:
                return ResultSprites[1];

            case RESULT.Lose:
                return ResultSprites[2];

            default:
                return null;
        }
    }
}
