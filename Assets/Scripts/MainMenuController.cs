using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        quitButton.onClick.AddListener(GameController.QuitGame);
        startButton.onClick.AddListener(GameController.StartGame);
        PlayerHealth.onGameOver += setGameOver;
    }

    public void OnDestroy()
    {
        if (quitButton != null) quitButton.onClick.RemoveAllListeners();
        if (startButton != null) startButton.onClick.RemoveAllListeners();
        PlayerHealth.onGameOver -= setGameOver;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void setGameOver()
    {
        Memory.isGameOver = true;
    }

}
