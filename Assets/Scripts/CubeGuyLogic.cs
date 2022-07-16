using System;
using UnityEngine;

public class CubeGuyLogic : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private SpriteRenderer _spriteTop;
    [SerializeField] private SpriteRenderer _spriteSide;
    [SerializeField] private SpriteRenderer _spriteFront;
    [SerializeField] private AudioClip _audioClipMove;

    private AudioSource _audioSource;
    private Vector2Int _boardPosition;
    private Input _input;
    private FaceColor _faceTop = FaceColor.Red;
    private FaceColor _faceSide = FaceColor.Green;
    private FaceColor _faceFront = FaceColor.Blue;

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
    }

    private void ColorFaces()
    {
        _spriteTop.color = FaceColorHelper.GetColorFromFaceColor(_faceTop);
        _spriteFront.color = FaceColorHelper.GetColorFromFaceColor(_faceFront);
        _spriteSide.color = FaceColorHelper.GetColorFromFaceColor(_faceSide);
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
}
