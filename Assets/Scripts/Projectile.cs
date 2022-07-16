using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private bool _isEnemy;
        private int _potency;
        private bool _isAlive;

        public bool PausePleaseThanks;

        public void Initialize(Vector3 worldPosition, int potency, bool isEnemy)
        {
            _isEnemy = isEnemy;
            transform.position = worldPosition;
            _potency = potency;

            transform.localScale = new Vector3(_isEnemy ? 1 : -1, 1, 1);

            _isAlive = true;
        }

        private void FixedUpdate()
        {
            if (PausePleaseThanks)
            {
                return;
            }

            if (!_isAlive)
            {
                Destroy(gameObject); // backup I guess...

                return;
            }

            if (GetComponentInChildren<SpriteRenderer>()?.isVisible == false) // outside of screen
            {
                Destroy(gameObject);

                return;
            }

            int direction = _isEnemy ? -1 : 1;
            float newX = transform.position.x + (Time.fixedDeltaTime * _speed * direction);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided!");

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
                else
                {
                    Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
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
