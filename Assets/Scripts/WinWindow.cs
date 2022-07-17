using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Awake()
    {
        FindObjectOfType<CubeGuyLogic>().enabled = false;
        Time.timeScale = 0f;

        StartCoroutine(NextSceneCoroutine());
    }

    private IEnumerator NextSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(_delay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
