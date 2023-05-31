using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController
{
    public delegate void Pause();
    public static Pause onPause;
    public delegate void Unpause();
    public static Unpause onResume;
    public delegate void Dead();
    public static Dead onDead;

    public GameController()
    {
    }

    public static void PauseGameWithoutEvent()
    {
        Time.timeScale = 0;
        Memory.isPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void PauseGame()
    {
        PauseGameWithoutEvent();

        if (onPause != null)
        {
            onPause.Invoke();
        }
    }

    public static void UnpauseGameWithoutEvent()
    {
        Time.timeScale = 1f;
        Memory.isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void UnpauseGame()
    {
        UnpauseGameWithoutEvent();

        if (onResume != null)
        {
            onResume.Invoke();
        }
    }

    public static void StartGame()
    {
        ResetMemory.Reset();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("OutsideChurch");
        UnpauseGame();
    }

    public static void RestartGame()
    {
        ResetMemory.Reset();
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("MainMenu");
        UnpauseGame();
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public static void PauseGameToggle()
    {
        if (!Memory.isPause)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    public static void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;

        Memory.isPause = true;
        if (onDead != null)
        {
            onDead.Invoke();
        }
    }
}
