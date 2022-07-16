using UnityEngine;

public class Board : MonoBehaviour
{
    private readonly Vector2 _toWorldMultiplier = new Vector2(2.0f, -1.5f);

    private readonly Vector2Int _playerMatrixStart = Vector2Int.zero;
    private readonly Vector2Int _playerMatrixEnd = new Vector2Int(2, 2);
    private readonly Vector2Int _enemyMatrixStart = new Vector2Int(3, 0);
    private readonly Vector2Int _enemyMatrixEnd = new Vector2Int(5, 5);

    public Vector2 BoardPositionToWorldPosition(Vector2Int boardPosition)
    {
        Vector2 position = boardPosition * _toWorldMultiplier;

        return position;
    }

    public Vector2Int GetMoveAttemptPosition(Vector2Int startPosition, Direction direction, bool player)
    {
        Vector2Int movement = GetMovement(direction);

        Vector2Int resultPosition = startPosition + movement;

        if (player)
        {
            resultPosition.Clamp(_playerMatrixStart, _playerMatrixEnd);
        }
        else
        {
            resultPosition.Clamp(_enemyMatrixStart, _enemyMatrixEnd);
        }

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
