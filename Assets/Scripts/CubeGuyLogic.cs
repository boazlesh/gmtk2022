using Assets;
using Assets.Scripts;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeGuyLogic : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private SpriteRenderer _spriteTop;
    [SerializeField] private SpriteRenderer _spriteSide;
    [SerializeField] private SpriteRenderer _spriteFront;
    [SerializeField] private AudioClip _audioClipMove;
    [SerializeField] private FaceColorMapping _faceColorMapping;
    [SerializeField] private CustomScreen _customScreen;
    [SerializeField] private PlayerHud _playerHud;
    [SerializeField] public Transform _projectilePosition;
    [SerializeField] private WinWindow _winWindowPrefab;

    private AudioSource _audioSource;
    private Animator _animator;
    private HealthComponent _healthComponent;
    private SpriteRenderer _eyesSpriteRenderer;

    public event Action OnCustomScreenEnter;
    public event Action OnCustomScreenExit;

    public Vector2Int _boardPosition;
    private Input _input;
    private FaceColor _faceTop = FaceColor.Red;
    private FaceColor _faceSide = FaceColor.Green;
    private FaceColor _faceFront = FaceColor.Blue;
    private bool _isMoveAnimationPlaying;
    private Direction? _bufferedMoveDirection;
    private bool _isCustomScreenAvailable = true;
    private bool _isInCustomScreen = false;
    private Dictionary<FaceColor, ActionInstance> _actionInstances = new Dictionary<FaceColor, ActionInstance>();

    public void Awake()
    {
        _audioSource = transform.GetComponent<AudioSource>();
        _animator = transform.GetComponentInChildren<Animator>();
        _healthComponent = transform.GetComponent<HealthComponent>();
        _eyesSpriteRenderer = transform.Find("SpritesHolder").Find("EyesHolder").GetComponent<SpriteRenderer>();

        ColorFaces();

        _input = new Input();

        _input.BattleActionMap.MoveUp.performed += _ => Move(Direction.Up);
        _input.BattleActionMap.MoveDown.performed += _ => Move(Direction.Down);
        _input.BattleActionMap.MoveLeft.performed += _ => Move(Direction.Left);
        _input.BattleActionMap.MoveRight.performed += _ => Move(Direction.Right);
        _input.BattleActionMap.CustomScreen.performed += _ => EnterCustomScreenIfPossible();
        _input.BattleActionMap.UseTopAbility.performed += _ => UseTopAbility();

        _playerHud._customGauge.OnCustomGaugeComplete += OnCustomGaugeComplete;

        PauseMenu.OnGamePause += () => _input.Disable();
        PauseMenu.OnGameResumed += () => _input.Enable();

        _healthComponent.OnHurt += OnHurt;
    }

    public void Win()
    {
        Instantiate(_winWindowPrefab);
    }

    public void Lose()
    {
        // TODO
    }

    private IEnumerator Start()
    {
        _input.Disable();

        yield return FadeEnemiesInRoutine();

        EnterCustomScreenIfPossible();

        foreach (EnemyLogic enemy in FindObjectsOfType<EnemyLogic>())
        {
            enemy.Act();
        }

        _input.Enable();
    }

    private IEnumerator FadeEnemiesInRoutine()
    {
        var enemies = FindObjectsOfType<EnemyLogic>().OrderBy(e => e._boardPosition.y);

        foreach (EnemyLogic enemy in enemies)
        {
            var color = enemy._spriteRenderer.material.color;

            enemy._spriteRenderer.material.color = new Color(color.r, color.g, color.b, a: 0);
        }

        yield return new WaitForSeconds(1f);

        foreach (EnemyLogic enemy in enemies)
        {
            yield return FadeToRoutine(enemy._spriteRenderer, 1, 1f);
        }
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnDestroy()
    {
        _input.Disable();

        StopAllCoroutines();
    }

    private void ColorFaces()
    {
        _spriteTop.color = _faceColorMapping.GetColorFromFaceColor(_faceTop, !_actionInstances.ContainsKey(_faceTop));
        _spriteFront.color = _faceColorMapping.GetColorFromFaceColor(_faceFront, !_actionInstances.ContainsKey(_faceFront));
        _spriteSide.color = _faceColorMapping.GetColorFromFaceColor(_faceSide, !_actionInstances.ContainsKey(_faceSide));

        _playerHud.UpdateActions(_actionInstances);
        _eyesSpriteRenderer.color = Color.white;
    }

    [ContextMenu("Color All Faces")]
    private void ColorFacesDebug()
    {
        _spriteTop.color = _faceColorMapping.GetColorFromFaceColor(_faceTop, false);
        _spriteFront.color = _faceColorMapping.GetColorFromFaceColor(_faceFront, false);
        _spriteSide.color = _faceColorMapping.GetColorFromFaceColor(_faceSide, false);
    }

    private void Move(Direction direction)
    {
        if (_isInCustomScreen) // somehow
        {
            return;
        }

        if (_isMoveAnimationPlaying)
        {
            _bufferedMoveDirection = direction;

            return;
        }

        _bufferedMoveDirection = null;

        ActuallyMove(direction);
    }

    private void ActuallyMove(Direction direction)
    {
        if (!MoveOnBoard(direction))
        {
            return;
        }

        StartCoroutine(MoveCoroutine(direction));
    }

    private IEnumerator MoveCoroutine(Direction direction)
    {
        yield return CoolShitWhenMoving(direction);

        transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);
        RotateCube(direction);

        //if (_bufferedMoveDirection.HasValue)
        //{
        //    ActuallyMove(_bufferedMoveDirection.Value);
        //    _bufferedMoveDirection = null;
        //}
    }

    private IEnumerator CoolShitWhenMoving(Direction direction)
    {
        _audioSource.PlayOneShot(_audioClipMove);

        _isMoveAnimationPlaying = true;

        // jam schuna
        string animationName = $"CubeRoll{direction}";
        _animator.Play(animationName, layer: 0);
        yield return new WaitForSeconds(0.25f); // nothing works... just wait the time you know it is jesus christ
        _animator.Play("Idle");
        yield return null;

        _animator.transform.localPosition = Vector3.zero;
        _animator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        _isMoveAnimationPlaying = false;
    }

    private bool MoveOnBoard(Direction direction)
    {
        Vector2Int movePosition = _board.AttemptToMove(_boardPosition, direction, player: true);

        if (_boardPosition == movePosition)
        {
            return false;
        }

        _boardPosition = movePosition;

        return true;
    }

    private void RotateCube(Direction direction)
    {
        FaceColor originalTop = _faceTop;
        FaceColor originalFront = _faceFront;
        FaceColor originalSide = _faceSide;

        switch (direction)
        {
            case Direction.Up:
            case Direction.Down:
                _faceTop = originalFront;
                _faceFront = originalTop;
                break;
            case Direction.Left:
            case Direction.Right:
                _faceTop = originalSide;
                _faceSide = originalTop;
                break;
            default:
                Debug.LogError($"{nameof(RotateCube)} with unknown direction: {direction}");
                break;
        }

        ColorFaces();
    }

    private void EnterCustomScreenIfPossible()
    {
        if (!_isCustomScreenAvailable)
        {
            return;
        }

        _isCustomScreenAvailable = false;
        StartCoroutine(EnterCustomScreenRoutine());
    }

    private IEnumerator EnterCustomScreenRoutine()
    {
        CoroutineResult<Dictionary<FaceColor, ActionInstance>> resultWrapper = new CoroutineResult<Dictionary<FaceColor, ActionInstance>>();

        _isInCustomScreen = true;

        _input.Disable();

        OnCustomScreenEnter?.Invoke();

        yield return _customScreen.SelectActionsRoutine().GetResult(resultWrapper);

        _isInCustomScreen = false;

        OnCustomScreenExit?.Invoke();

        _actionInstances = resultWrapper.Value;
        ColorFaces();

        _input.Enable();

        _isCustomScreenAvailable = false;
        _playerHud._customGauge.RunCustomGauge();
    }

    private void UseTopAbility()
    {
        if (_isMoveAnimationPlaying)
        {
            return;
        }

        StartCoroutine(UseTopAbilityCoroutine());
    }

    private IEnumerator UseTopAbilityCoroutine()
    {
        if (!_actionInstances.ContainsKey(_faceTop))
        {
            yield break;
        }

        var abilityInstance = _actionInstances[_faceTop];

        _actionInstances.Remove(_faceTop);
        ColorFaces();

        if (abilityInstance.Action._projectilePrefab == null)
        {
            yield break;
        }

        Projectile projectile = Instantiate(abilityInstance.Action._projectilePrefab, parent: null);
        projectile.Initialize(_projectilePosition.transform.position, potency: abilityInstance.Potency, isEnemy: false);

        if (abilityInstance.Action._heal)
        {
            GetComponent<HealthComponent>().Heal(abilityInstance.Potency);
        }
    }

    private void OnCustomGaugeComplete()
    {
        _isCustomScreenAvailable = true;
    }

    private IEnumerator FadeToRoutine(SpriteRenderer spriteRenderer, float targetAlpha, float timeSeconds)
    {
        Color color = spriteRenderer.material.color;

        float original = color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeSeconds)
        {
            Color newColor = new Color(color.r, color.g, color.b, Mathf.Lerp(original, targetAlpha, t));
            spriteRenderer.material.color = newColor;
            yield return null;
        }
    }

    private void OnHurt()
    {
        StartCoroutine(HurtAnimationCoroutine());
    }

    private IEnumerator HurtAnimationCoroutine()
    {
        var originalColor = _eyesSpriteRenderer.color;
        _eyesSpriteRenderer.color = Color.Lerp(originalColor, Color.red, 0.5f);

        AudioClipOneShotPlayer.SpawnOneShot(_healthComponent._audioClipHurt);

        yield return new WaitForSeconds(0.1f);

        _eyesSpriteRenderer.color = originalColor;
    }
}
