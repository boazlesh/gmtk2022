using Assets.Scripts;
using System.Collections;
using UnityEngine;

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

        var levelLoader = FindObjectOfType<LevelLoader>();

        if (levelLoader == null)
        {
            yield break;
        }

        levelLoader.LoadNextLevel();
    }
}
