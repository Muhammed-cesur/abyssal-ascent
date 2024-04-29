using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Image _HealthBarSlider;
    

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        _HealthBarSlider.fillAmount = CurrentHealth / MaxHealth;
    }
}
