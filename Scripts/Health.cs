using UnityEngine;
using System;

public class Health : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public event Action<float> HealthChanged;

    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0f, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0f, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }
}