using UnityEngine;

public class Board : MonoBehaviour
{
    private readonly Vector2 _toWorldMultiplier = new Vector2(2.0f, -1.5f);

    private readonly GameObject[,] _matrix = new GameObject[3, 3];
    private readonly Vector2Int _matrixSize;

    public Board()
    {
        _matrixSize = new Vector2Int(_matrix.GetLength(0) - 1, _matrix.GetLength(1) - 1);
    }

    public Vector2 BoardPositionToWorldPosition(Vector2Int boardPosition)
    {
        return boardPosition * _toWorldMultiplier;
    }

    public Vector2Int GetMoveAttemptPosition(Vector2Int startPosition, Direction direction)
    {
        Vector2Int movement = GetMovement(direction);

        Vector2Int resultPosition = startPosition + movement;

        resultPosition.Clamp(Vector2Int.zero, _matrixSize);

        return resultPosition;
    }

    private Vector2Int GetMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2Int.down;
            case Direction.Down:
                return Vector2Int.up;
            case Direction.Left:
                return Vector2Int.left;
            case Direction.Right:
                return Vector2Int.right;
            default:
                Debug.LogError($"{nameof(GetMovement)} with unknown direction: {direction}");
                return Vector2Int.zero;
        }
    }
}
