using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutButton : MonoBehaviour
{
    private const int LOBBY_SCENE_NUMBER = 0;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
            LoadLobby
            );
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene(LOBBY_SCENE_NUMBER);
    }
}
