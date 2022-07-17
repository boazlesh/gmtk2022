using Assets;
using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Transform _rewardWindow;

    private Input _input;

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

        var reward = FindObjectOfType<Reward>();
        if (reward != null)
        {
            _rewardWindow.gameObject.SetActive(true);

            var action = reward._actionModel;

            _image.sprite = action._sprite;
            _name.text = action._displayName;
            _description.text = action._text;
        }

        _input = new Input();

        _input.BattleActionMap.Continue.performed += ContinuePerformed;

        _input.Enable();
    }

    private void ContinuePerformed(InputAction.CallbackContext obj)
    {
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
