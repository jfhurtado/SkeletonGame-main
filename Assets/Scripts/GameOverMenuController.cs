using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Button restartButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
        GameController.onDead += MenuDisplayToggle;
        PlayerHealth.onGameOver += GameController.GameOver;
        quitButton.onClick.AddListener(GameController.QuitGame);
        restartButton.onClick.AddListener(GameController.RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDestroy()
    {
        PlayerHealth.onGameOver -= GameController.GameOver;
        GameController.onDead -= MenuDisplayToggle;
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    private void MenuDisplayToggle()
    {
        if (Memory.isGameOver)
        {
            gameOverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gameOverMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
