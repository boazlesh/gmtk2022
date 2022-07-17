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
        FindObjectOfType<CubeGuyLogic>().enabled = false;

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
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
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
