using Assets.Scripts;
using Assets.Scripts.Utils;
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
        private bool _isAlive = true;
        private bool _isTurnActive = true;

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
            _board.Place(_boardPosition, gameObject);
            _player.OnCustomScreenEnter += OnCustomScreenEnter;
            _player.OnCustomScreenExit += OnCustomScreenExit;

            StartCoroutine(ActRoutine());
        }

        private IEnumerator ActRoutine()
        {
            while (_isAlive)
            {
                yield return new WaitUntil(() => _isTurnActive);
                yield return new WaitForSeconds(_actionCooldownSeconds);
                
                if (!_isTurnActive)
                {
                    continue;
                }

                Direction? movementDirection = ChooseMovementDirection();

                if (movementDirection != null)
                {
                    CoroutineResult<MovementResult> movementResult = new CoroutineResult<MovementResult>();
                    yield return PerformMovementRoutine(movementDirection.Value).GetResult(movementResult);

                    if (movementResult.Value.DidMove)
                    {
                        // If moved, don't also try to perform an action
                        continue;
                    }
                }

                yield return PerformActionRoutine();
            }
        }

        private Direction? ChooseMovementDirection()
        {
            switch (_actionInstance.Action._actionType)
            {
                case ActionType.Cannon:
                    {
                        if (_boardPosition.y > _player._boardPosition.y)
                        {
                            return Direction.Up;
                        }
                        else if (_boardPosition.y < _player._boardPosition.y)
                        {
                            return Direction.Down;
                        }
                        else
                        {
                            return null;
                        }
                    }
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
            Vector2Int movePosition = _board.AttemptToMove(_boardPosition, direction, player: false);

            if (_boardPosition == movePosition)
            {
                yield return new MovementResult { DidMove = false };
                yield break;
            }

            _boardPosition = movePosition;
            SyncWorldPositionToBoardPosition();

            yield return new MovementResult { DidMove = true };
            yield break;
        }

        private IEnumerator PerformActionRoutine()
        {
            yield return null;
        }

        private void SyncWorldPositionToBoardPosition()
        {
            transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);
        }

        private void OnCustomScreenEnter()
        {
            _isTurnActive = false;
        }

        private void OnCustomScreenExit()
        {
            _isTurnActive = true;
        }
    }
}
