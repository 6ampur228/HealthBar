using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthBar;

    private Coroutine _currentCoroutine;

    private bool _isCoroutineEnd = true;

    private void Start()
    {
        _healthBar.value = _health.CurrentHealth;
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (_isCoroutineEnd)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(ChangeSliderValue(currentHealth));
        }
    }

    private IEnumerator ChangeSliderValue(float currentHealth)
    {
        _isCoroutineEnd = false;

        if (currentHealth <= _healthBar.maxValue && currentHealth >= _healthBar.minValue)
        {
            while (_healthBar.value != currentHealth)
            {
                _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentHealth, Time.fixedDeltaTime);

                yield return null;
            }
        }

        _isCoroutineEnd = true;
    }
}