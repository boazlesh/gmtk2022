using Assets;
using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Awake()
    {
        CubeGuyLogic player = FindObjectOfType<CubeGuyLogic>();
        if (player != null)
        {
            player.enabled = false;
        }

        var projectiles = FindObjectsOfType<Projectile>();
        foreach (var projectile in projectiles)
        {
            Destroy(projectile);
        }

        CustomGauge customGauge = FindObjectOfType<CustomGauge>();
        if (customGauge != null)
        {
            customGauge.enabled = false;
        }

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
