using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthDisplay _healthDisplay;

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
        }
    }

    [ContextMenu("Take 1 Damage")]
    public void TakeDamageEditor()
    {
        TakeDamage(1);
    }

    public void Heal(int heal)
    {
        CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + heal);
    }

    private void Die()
    {

    }
}
