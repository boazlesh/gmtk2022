using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
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

            transform.localScale = new Vector3(_speed >= 0 ? -1 : 1, 1, 1);

            _isAlive = true;
        }

        private void FixedUpdate()
        {
            if (!_isAlive)
            {
                return;
            }

            float newX = transform.position.x + Time.fixedDeltaTime * _speed;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided!");

            // TODO: collision with Killzone

            if (collision.gameObject.GetComponent<Projectile>() != null)
            {
                return;
            }

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
                    enemy.GetComponent<HealthComponent>().TakeDamage(_potency);

                    _isAlive = false;
                }
            }

            if (!_isAlive)
            {
                Destroy(gameObject);
            }
        }
    }
}
