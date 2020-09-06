using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _targetHealthIndex;

    private Slider _slider;
    private Image _fill;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _fill = GameObject.FindGameObjectWithTag("Filler").GetComponent<Image>();
    }

    private void Update()
    {
        _slider.DOValue(_targetHealthIndex, 3);

        if (_slider.value < 0.3)
            _fill.DOColor(Color.red, 3).SetEase(Ease.Linear);
        else if (_slider.value < 0.7)
            _fill.DOColor(Color.yellow, 3).SetEase(Ease.Linear);
        else
            _fill.DOColor(Color.green,3).SetEase(Ease.Linear);
    }

    public void ApplyDamage(float value)
    {
        float currentHealth = _maxHealth * _slider.value;
        currentHealth -= value;

        _targetHealthIndex = currentHealth < 0 ?
            _targetHealthIndex = 0 :
            _targetHealthIndex = currentHealth / _maxHealth;
    }

    public void ApplyHeal(float value)
    {
        float currentHealth = _maxHealth * _targetHealthIndex;
        currentHealth += value;

        _targetHealthIndex = currentHealth > _maxHealth ?
            _targetHealthIndex = 1 :
            _targetHealthIndex = currentHealth / _maxHealth;
    }
}
