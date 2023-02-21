using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    private Coroutine _currentCoroutine;

    private bool _isCoroutineEnd = true;

    public void ChangeValue(float value)
    {
        if (_isCoroutineEnd)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(ChangeSliderValue(value));
        }       
    }

    private IEnumerator ChangeSliderValue(float value)
    {
        float endValue = _healthBar.value + value;

        _isCoroutineEnd = false;

        if(endValue <= _healthBar.maxValue && endValue >= _healthBar.minValue)
        {
            while (_healthBar.value != endValue)
            {
                _healthBar.value = Mathf.MoveTowards(_healthBar.value, endValue, Time.fixedDeltaTime);

                yield return null;
            }
        }

        _isCoroutineEnd = true;
    }
}
