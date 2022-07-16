using Assets.Scripts;
using System.Collections;
using UnityEngine;

namespace Assets
{
    public class EnemyLogic : MonoBehaviour
    {
        [SerializeField] private float _actionCooldownSeconds = 1f;
        [SerializeField] private ActionInstance _actionInstance;
        [SerializeField] public Vector2Int _boardPosition;

        private CubeGuyLogic _player;
        private Board _board;
        private bool _isActive = true;

        private void Awake()
        {
            _board = FindObjectOfType<Board>();
            _player = FindObjectOfType<CubeGuyLogic>();
        }

        private void OnValidate()
        {
            _board = FindObjectOfType<Board>();
            SyncWorldPositionToBoardPosition();
        }

        private void Start()
        {
            StartCoroutine(ActRoutine());
        }

        private IEnumerator ActRoutine()
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(_actionCooldownSeconds);

                Direction? movementDirection = ChooseMovementDirection();

                if (movementDirection != null)
                {
                    yield return PerformMovementRoutine(movementDirection.Value);
                }

                yield return PerformActionRoutine();
            }
        }

        private Direction? ChooseMovementDirection()
        {
            switch (_actionInstance.Action._actionType)
            {
                case ActionType.Cannon:
                case ActionType.Sword:
                    {
                        if (_boardPosition.y > _player._boardPosition.y)
                        {
                            return Direction.Up;
                        }
                        else if (_boardPosition.y < _player._boardPosition.y)
                        {
                            return Direction.Down;
                        }
                        else if (_boardPosition.x > 3)
                        {
                            return Direction.Left;
                        }
                        else
                        {
                            return null;
                        }
                    }
                case ActionType.Heal:
                default:
                    return null;
            }
        }

        private IEnumerator PerformMovementRoutine(Direction direction)
        {
            Vector2Int movePosition = _board.GetMoveAttemptPosition(_boardPosition, direction, player: false);

            if (_boardPosition == movePosition)
            {
                yield return false;
                yield break;
            }

            _boardPosition = movePosition;
            SyncWorldPositionToBoardPosition();

            yield return true;
            yield break;
        }

        private IEnumerator PerformActionRoutine()
        {
            return null;
        }

        private void SyncWorldPositionToBoardPosition()
        {
            transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);
        }
    }
}
