using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowLose : MonoBehaviour
{
    private Input _input;

    private void Awake()
    {
        Time.timeScale = 0f;

        var player = FindObjectOfType<CubeGuyLogic>();
        if (player != null)
        {
            player.enabled = false;
        }

        _input = new Input();
        _input.Enable();
        _input.BattleActionMap.UseTopAbility.performed += _ => RestartLevel();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        _input.Disable();
        PauseMenu.RestartLevel();
    }
}
