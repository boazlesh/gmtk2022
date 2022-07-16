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

    private AudioSource _audioSource;
    private Vector2Int _boardPosition;
    private Input _input;
    private FaceColor _faceTop = FaceColor.Red;
    private FaceColor _faceSide = FaceColor.Green;
    private FaceColor _faceFront = FaceColor.Blue;
    private bool _isCustomScreenAvailable = true;
    private bool _isInCustomScreen = false;
    private List<ActionInstance> _actionInstances = new List<ActionInstance>();

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        ColorFaces();

        _input = new Input();
        _input.Enable();

        _input.BattleActionMap.MoveUp.performed += _ => Move(Direction.Up);
        _input.BattleActionMap.MoveDown.performed += _ => Move(Direction.Down);
        _input.BattleActionMap.MoveLeft.performed += _ => Move(Direction.Left);
        _input.BattleActionMap.MoveRight.performed += _ => Move(Direction.Right);
        _input.BattleActionMap.CustomScreen.performed += _ => EnterCustomScreenIfPossible();
    }

    private void ColorFaces()
    {
        _spriteTop.color = _faceColorMapping.GetColorFromFaceColor(_faceTop);
        _spriteFront.color = _faceColorMapping.GetColorFromFaceColor(_faceFront);
        _spriteSide.color = _faceColorMapping.GetColorFromFaceColor(_faceSide);
    }

    private void Move(Direction direction)
    {
        if (!MoveOnBoard(direction))
        {
            return;
        }

        _audioSource.PlayOneShot(_audioClipMove);

        RotateCube(direction);
    }

    private bool MoveOnBoard(Direction direction)
    {
        Vector2Int movePosition = _board.GetMoveAttemptPosition(_boardPosition, direction);

        if (_boardPosition == movePosition)
        {
            return false;
        }

        _boardPosition = movePosition;
        transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);

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
        CoroutineResult<List<ActionInstance>> resultWrapper = new CoroutineResult<List<ActionInstance>>();

        _isInCustomScreen = true;

        _input.Disable();

        yield return _customScreen.SelectActionsRoutine().GetResult(resultWrapper);

        _isInCustomScreen = false;

        _actionInstances = resultWrapper.Value;

        _input.Enable();

    }
}
