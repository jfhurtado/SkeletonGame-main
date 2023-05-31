using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        GameController.onPause += MenuDisplayToggle;
        GameController.onResume += MenuDisplayToggle;
        resumeButton.onClick.AddListener(GameController.UnpauseGame);
        quitButton.onClick.AddListener(GameController.QuitGame);
        restartButton.onClick.AddListener(GameController.RestartGame);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !Memory.isGameOver && !Memory.isDuringTutorial)
        {
            GameController.PauseGameToggle();
            if (Memory.isPause)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            return;
        }
    }

    public void OnDestroy()
    {
        GameController.onPause -= MenuDisplayToggle;
        GameController.onResume -= MenuDisplayToggle;
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
    }

    private void MenuDisplayToggle()
    {
        if (Memory.isPause)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
