using Assets.Scripts;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Color _mutedColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);
    [SerializeField] private float _mutedColorStrength = 0.75f;

    private AudioSource _audioSource;
    private Animator _animator;

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
        _audioSource = GetComponent<AudioSource>();
        _animator = transform.GetComponentInChildren<Animator>();

        ColorFaces();

        _input = new Input();

        _input.BattleActionMap.MoveUp.performed += _ => Move(Direction.Up);
        _input.BattleActionMap.MoveDown.performed += _ => Move(Direction.Down);
        _input.BattleActionMap.MoveLeft.performed += _ => Move(Direction.Left);
        _input.BattleActionMap.MoveRight.performed += _ => Move(Direction.Right);
        _input.BattleActionMap.CustomScreen.performed += _ => EnterCustomScreenIfPossible();
        _input.BattleActionMap.UseTopAbility.performed += _ => UseTopAbility();

        PauseMenu.OnGamePause += () => _input.Disable();
        PauseMenu.OnGameResumed += () => _input.Enable();
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
        _spriteTop.color = _faceColorMapping.GetColorFromFaceColor(_faceTop);
        _spriteFront.color = _faceColorMapping.GetColorFromFaceColor(_faceFront);
        _spriteSide.color = _faceColorMapping.GetColorFromFaceColor(_faceSide);

        if (!_actionInstances.ContainsKey(_faceTop))
        {
            _spriteTop.color = Color.Lerp(_spriteTop.color, _mutedColor, _mutedColorStrength);
        }
        if (!_actionInstances.ContainsKey(_faceFront))
        {
            _spriteFront.color = Color.Lerp(_spriteFront.color, _mutedColor, _mutedColorStrength);
        }
        if (!_actionInstances.ContainsKey(_faceSide))
        {
            _spriteSide.color = Color.Lerp(_spriteSide.color, _mutedColor, _mutedColorStrength);
        }
    }

    private void Move(Direction direction)
    {
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

        StartCoroutine(EnterCustomScreenRoutine());
    }

    private IEnumerator EnterCustomScreenRoutine()
    {
        CoroutineResult<Dictionary<FaceColor, ActionInstance>> resultWrapper = new CoroutineResult<Dictionary<FaceColor, ActionInstance>>();

        _isInCustomScreen = true;

        _input.Disable();

        yield return _customScreen.SelectActionsRoutine().GetResult(resultWrapper);

        _isInCustomScreen = false;

        _actionInstances = resultWrapper.Value;
        ColorFaces();

        _input.Enable();
    }

    private void UseTopAbility()
    {
        if (!_actionInstances.ContainsKey(_faceTop))
        {
            return;
        }

        var abilityInstance = _actionInstances[_faceTop];

        _actionInstances.Remove(_faceTop);
        ColorFaces();
    }
}
