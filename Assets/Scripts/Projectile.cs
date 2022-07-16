using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private const float _waitBetweenMove = 0.1f;

        private bool _isEnemy;
        private int _potency;
        private float _speed;
        private bool _isAlive;

        public void Initialize(Vector3 worldPosition, float speed, int potency, bool isEnemy)
        {
            _isEnemy = isEnemy;
            transform.position = worldPosition;
            _speed = speed;
            _potency = potency;

            _isAlive = true;

            StartCoroutine(MoveRoutine());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided!");

            if (_isEnemy)
            {
                CubeGuyLogic player = collision.transform.GetComponentInParent<CubeGuyLogic>();

                if (player != null)
                {
                    player.GetComponent<HealthComponent>().TakeDamage(_potency);

                    _isAlive = false;
                }
            }
            else
            {
                EnemyLogic enemy = collision.gameObject.GetComponentInParent<EnemyLogic>();

                if (enemy != null)
                {
                    //enemy.Damage(_potency);

                    _isAlive = false;
                }
            }

            Destroy(gameObject);
        }

        private IEnumerator MoveRoutine()
        {
            while (_isAlive)
            {
                yield return new WaitForSeconds(_waitBetweenMove);

                float newX = transform.position.x + Time.deltaTime * _speed * 100;

                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
    }
}
