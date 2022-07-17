using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static event Action OnGamePause;
    public static event Action OnGameResumed;

    public static bool IsGamePaused = false;

    [SerializeField] private GameObject _pauseMenu;

    private Input _input;

    private void Awake()
    {
        _input = new Input();
        _input.BattleActionMap.Pause.performed += _ => OnPausePerformed();
        _input.Enable();
    }

    public static void RestartLevel()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public static IEnumerator RestartLevelAsync()
    {
        Resources.UnloadUnusedAssets();
        AsyncOperation loadLevelTask =
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        while (!loadLevelTask.isDone)
        {
            yield return null;
        }
    }

    public void ResumeAndRestartLevel()
    {
        RestartLevel();
        Resume();
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;

        OnGameResumed?.Invoke();
    }

    private void OnPausePerformed()
    {
        Debug.Log("Pause clicked");

        if (IsGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;

        OnGamePause?.Invoke();
    }
}
