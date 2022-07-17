﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _ttl;
        [SerializeField] private bool _canKillOtherProjectiles;
        [SerializeField] private AudioClip _audioClipSpawn;
        [SerializeField] private bool _doesBreakOnImpact = true;

        private static AudioClip _audioClipKillOtherProjectile;

        private bool _isEnemy;
        private int _potency;
        private bool _isAlive;
        private float _timeAlive;

        [System.NonSerialized] public bool PausePleaseThanks;

        public void Initialize(Vector3 worldPosition, int potency, bool isEnemy)
        {
            _isEnemy = isEnemy;
            transform.position = worldPosition;
            _potency = potency;

            transform.localScale = new Vector3(_isEnemy ? 1 : -1, 1, 1);

            _timeAlive = 0.0f;
            _isAlive = true;

            if (_audioClipSpawn != null)
            {
                AudioClipOneShotPlayer.SpawnOneShot(_audioClipSpawn);
            }
        }

        private void FixedUpdate()
        {
            if (PausePleaseThanks)
            {
                return;
            }

            if (_timeAlive > _ttl)
            {
                _isAlive = false;
            }

            if (!_isAlive)
            {
                Destroy(gameObject);

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

            _timeAlive += Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isEnemy)
            {
                CubeGuyLogic player = collision.transform.GetComponentInParent<CubeGuyLogic>();

                if (player != null)
                {
                    player.GetComponent<HealthComponent>().TakeDamage(_potency);

                    if (_doesBreakOnImpact)
                    {
                        _isAlive = false;
                    }
                }
            }
            else
            {
                EnemyLogic enemy = collision.gameObject.GetComponentInParent<EnemyLogic>();

                if (enemy != null)
                {
                    enemy.GetComponent<HealthComponent>().TakeDamage(_potency);

                    if (_doesBreakOnImpact)
                    {
                        _isAlive = false;
                    }
                }
            }

            if (_isAlive)
            {
                if (_canKillOtherProjectiles)
                {
                    Projectile otherProjectile = collision.gameObject.GetComponentInParent<Projectile>();

                    if (otherProjectile != null)
                    {
                        if (_audioClipKillOtherProjectile == null)
                        {
                            _audioClipKillOtherProjectile = Resources.Load<AudioClip>("SFX/destroy_other_projectile");
                        }

                        AudioClipOneShotPlayer.SpawnOneShot(_audioClipKillOtherProjectile);
                        Destroy(collision.gameObject);
                    }
                }

                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
