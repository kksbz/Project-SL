using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    float _healthPoint;
    float _maxHealthPoint;
    float _staminaPoint;
    float _maxStaminaPoint;
    float _manaPoint;
    float _maxManaPoint;

    public HealthSystem() 
    {
        _healthPoint        = 0;
        _maxHealthPoint     = 0;
        _staminaPoint       = 0;
        _maxStaminaPoint    = 0;
        _manaPoint          = 0;
        _maxManaPoint       = 0;  
    }

    public float HP { get { return _healthPoint; } }
    public float MaxHP { get { return _maxHealthPoint; } }
    public float SP { get { return _staminaPoint; } }
    public float MaxSP { get { return _maxStaminaPoint; } }
    public float MP { get { return _manaPoint;} }
    public float MaxMP { get { return _maxManaPoint;} }

    #region Increase, Decrease Health
    public void Increase_HP(float value_)
    {
        _healthPoint = Mathf.Clamp(_healthPoint + value_, 0f, _maxHealthPoint);
    }
    public void Decrease_HP(float value_)
    {
        _healthPoint = Mathf.Clamp(_healthPoint - value_, 0f, _maxHealthPoint);
    }
    public void Increase_SP(float value_)
    {
        _staminaPoint = Mathf.Clamp(_staminaPoint + value_, 0f, _maxStaminaPoint);
    }
    public void Decrease_SP(float value_)
    {
        _staminaPoint = Mathf.Clamp(_staminaPoint - value_, 0f, _maxStaminaPoint);
    }
    public void Increase_MP(float value_)
    {
        _manaPoint = Mathf.Clamp(_manaPoint + value_, 0f, _maxManaPoint);
    }
    public void Decrease_MP(float value_)
    {
        _manaPoint = Mathf.Clamp(_manaPoint - value_, 0f, _maxManaPoint);
    }
    #endregion  // Increase Decrease Health
    #region Damage, Consumption

    void Damage(float damageAmount)
    {
        _healthPoint -= damageAmount;
        if(_healthPoint <= 0f)
        {
            // 죽음 처리?
        }
    }
    void ConsumSP(float value_)
    {
        _staminaPoint -= value_;
    }
    // 이후 추가?

    #endregion  // Damage, Consumption
}
