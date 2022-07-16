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
        [SerializeField] public Transform _projectilePosition;
        [SerializeField] public Animator _animator;
        [SerializeField] public AudioClip _audioClipDeath;

        private CubeGuyLogic _player;
        private Board _board;
        private bool _isAlive = true;
        private bool _isTurnActive = true;

        private void Awake()
        {
            _board = FindObjectOfType<Board>();
            _player = FindObjectOfType<CubeGuyLogic>();

            transform.GetComponent<HealthComponent>().OnDied += OnDied;
        }

        private void OnValidate()
        {
            _board = FindObjectOfType<Board>();
            transform.localPosition = _board.BoardPositionToWorldPosition(_boardPosition);
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
                case ActionType.Laser:
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
            yield return SyncWorldPositionToBoardPositionRoutine();

            yield return new MovementResult { DidMove = true };
            yield break;
        }

        private IEnumerator SmoothLerp(Vector3 startPosition, Vector3 endPosition, float timeSeconds)
        {
            float elapsedTime = 0f;

            while (elapsedTime < timeSeconds)
            {
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, (elapsedTime / timeSeconds));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator PerformActionRoutine()
        {
            if (_actionInstance.Action._projectilePrefab == null)
            {
                yield break;
            }

            _animator.SetBool("attacking", true);

            Projectile projectile = Instantiate(_actionInstance.Action._projectilePrefab, parent: null);
            projectile.Initialize(_projectilePosition.transform.position, potency: _actionInstance.Potency, isEnemy: true);

            yield return new WaitForSeconds(1f);

            _animator.SetBool("attacking", false);
        }

        private IEnumerator SyncWorldPositionToBoardPositionRoutine()
        {
            Vector3 finalPosition = _board.BoardPositionToWorldPosition(_boardPosition);

            _animator.SetBool("walking", true);

            yield return SmoothLerp(transform.localPosition, finalPosition, timeSeconds: 0.5f);

            _animator.SetBool("walking", false);
        }

        private void OnCustomScreenEnter()
        {
            _isTurnActive = false;
        }

        private void OnCustomScreenExit()
        {
            _isTurnActive = true;
        }

        private void OnDied()
        {
            AudioClipOneShotPlayer.SpawnOneShot(_audioClipDeath);

            Destroy(gameObject);
        }
    }
}
