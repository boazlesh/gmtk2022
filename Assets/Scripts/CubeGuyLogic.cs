using System;
using UnityEngine;

public class CubeGuyLogic : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private SpriteRenderer _spriteTop;
    [SerializeField] private SpriteRenderer _spriteFront;
    [SerializeField] private SpriteRenderer _spriteSide;

    private Vector2Int _boardPosition;
    private Input _input;
    private FaceColor _faceTop = FaceColor.Red;
    private FaceColor _faceFront = FaceColor.Blue;
    private FaceColor _faceSide = FaceColor.Green;

    public void Awake()
    {
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
        MoveOnBoard(direction);
        RotateCube(direction);
    }

    private void MoveOnBoard(Direction direction)
    {
        Vector2Int movePosition = _board.GetMoveAttemptPosition(_boardPosition, direction);

        _boardPosition = movePosition;
        transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);
    }

    private void RotateCube(Direction direction)
    {

    }
}
