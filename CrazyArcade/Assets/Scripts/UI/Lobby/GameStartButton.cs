using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
            LoadStage
         );
    }

    public void LoadStage()
    {
        SceneManager.LoadScene(GameManager.Instance.SelectedStage.StageNumber);
    }
}
