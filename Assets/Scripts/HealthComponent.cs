using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action OnHurt;
    public event Action OnHealed;
    public event Action OnDied;

    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthDisplay _healthDisplay;
    [SerializeField] public AudioClip _audioClipHurt;

    private SpriteRenderer _spriteRenderer;
    private int _currentHealth;

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            _healthDisplay?.SetText(_currentHealth);
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        if (CurrentHealth == 0)
        {
            Die();

            return;
        }

        OnHurt?.Invoke();
    }

    [ContextMenu("Take 1 Damage")]
    public void TakeDamageEditor()
    {
        TakeDamage(1);
    }

    public void Heal(int heal)
    {
        CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + heal);

        OnHealed?.Invoke();
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
