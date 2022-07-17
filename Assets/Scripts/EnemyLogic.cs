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
        [SerializeField] public SpriteRenderer _spriteRenderer;

        private CubeGuyLogic _player;
        private Board _board;
        private bool _isAlive = true;
        private bool _isTurnActive = true;
        private HealthComponent _healthComponent;
        private Direction? _previousMovement;

        private void Awake()
        {
            _board = FindObjectOfType<Board>();
            _player = FindObjectOfType<CubeGuyLogic>();

            _healthComponent = transform.GetComponent<HealthComponent>();
            _healthComponent.OnDied += OnDied;
            _healthComponent.OnHurt += OnHurt;
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
        }

        public void Act()
        {
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
                        _previousMovement = movementDirection;

                        // If moved, don't also try to perform an action
                        continue;
                    }
                }

                _previousMovement = null;

                yield return PerformActionRoutine();
            }
        }

        private Direction? ChooseMovementDirection()
        {
            switch (_actionInstance.Action._actionType)
            {
                case ActionType.Laser:
                    if (_boardPosition.y == _board._enemyMatrixEnd.y)
                    {
                        if (_previousMovement.HasValue)
                        {
                            return null;
                        }

                        return Direction.Up;
                    }
                    else if (_boardPosition.y == _board._enemyMatrixStart.y)
                    {
                        if (_previousMovement.HasValue)
                        {
                            return null;
                        }

                        return Direction.Down;
                    }
                    else
                    {
                        return _previousMovement ?? Direction.Down;
                    }
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

        private void OnHurt()
        {
            StartCoroutine(HurtAnimationCoroutine());
        }

        private IEnumerator HurtAnimationCoroutine()
        {
            var originalPos = _spriteRenderer.transform.parent.localPosition;
            _spriteRenderer.transform.parent.localPosition = new Vector3(originalPos.x, originalPos.y + 0.15f, originalPos.z);

            var originalColor = _spriteRenderer.color;
            _spriteRenderer.color = Color.Lerp(originalColor, Color.red, 0.25f);

            AudioClipOneShotPlayer.SpawnOneShot(_healthComponent._audioClipHurt);

            yield return new WaitForSeconds(0.15f);

            _spriteRenderer.transform.parent.localPosition = originalPos;
            _spriteRenderer.color = originalColor;
        }
    }
}
