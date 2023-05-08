using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public enum EHealthChangeType : byte
{
    Init,
    Damage,
    Consum,
    Heal,
}
public enum EHealthType : byte
{
    HP,
    MP,
    SP
}

public class HealthSystem
{
    PlayerCharacter _playerCharacter;

    float _healthPoint;
    float _maxHealthPoint;
    float _staminaPoint;
    float _maxStaminaPoint;
    float _manaPoint;
    float _maxManaPoint;

    public float _staminaRegerationAmount = 20f;
    public float _staminaRegenMultiplier = 1f;
    public float _staminaRegenTimer = default;
    
    public delegate void HealthEventHandler();
    public delegate void HealthEventHandler_TwoParam(EHealthType type, bool isLerp);
    public HealthEventHandler onDieHandle;
    public HealthEventHandler_TwoParam onChangedHealth;
    

    public float HP { get { return _healthPoint; } set { _healthPoint = value; } }
    public float MaxHP { get { return _maxHealthPoint; } }
    public float SP { get { return _staminaPoint; } }
    public float MaxSP { get { return _maxStaminaPoint; } }
    public float MP
    {
        get { return _manaPoint; }
        set { _manaPoint = value; }
    }
    public float MaxMP { get { return _maxManaPoint; } }

    public HealthSystem(PlayerCharacter bindingPC) 
    {
        _playerCharacter = bindingPC;
        _healthPoint        = 0;
        _maxHealthPoint     = 0;
        _staminaPoint       = 0;
        _maxStaminaPoint    = 0;
        _manaPoint          = 0;
        _maxManaPoint       = 0;
        onChangedHealth = new HealthEventHandler_TwoParam(OnChangedHealth);
    }

    

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
    #region Damage, Consumption, Regeneration

    public void Damage(float damageAmount)
    {
        _healthPoint -= damageAmount;
        onChangedHealth(EHealthType.HP, true);
        if(IsDead())
        {
            onDieHandle();
            // ���� ó��?
        }
    }
    public void HealHP(float healAmount) 
    {
        _healthPoint = Mathf.Clamp(_healthPoint + healAmount, 0, _maxHealthPoint);
        onChangedHealth(EHealthType.HP, false);
    }
    public void HealMP(float healAmount)
    {
        _manaPoint = Mathf.Clamp(_manaPoint + healAmount, 0, _maxManaPoint);
        onChangedHealth(EHealthType.MP, false);
    }
    public void ConsumMP(float value_)
    {
        _manaPoint = Mathf.Clamp(_manaPoint - value_, 0, _maxManaPoint);
        onChangedHealth(EHealthType.MP, true);
    }
    public void ConsumSP(float value_)
    {
        _staminaPoint -= value_;
        onChangedHealth(EHealthType.SP, true);
    }
    public void RegenerationStamina()
    {
        if(_playerCharacter.combatController.IsPlayingRootMotion || _playerCharacter.StateMachine.IsRunPressed)
        {
            _staminaRegenTimer = 0f;
        }
        else
        {
            _staminaRegenTimer += Time.deltaTime;
            if (_staminaPoint < _maxStaminaPoint && _staminaRegenTimer > 1f)
            {
                _staminaPoint += _staminaRegerationAmount * _staminaRegenMultiplier * Time.deltaTime;
                onChangedHealth(EHealthType.SP, false);
            }
        }
    }
    void OnChangedHealth(EHealthType healthType, bool isLerp)
    {
        Image targetBar = default;
        Image targetDecBar = default;
        float targetRatio = 0f;
        switch(healthType)
        {
            case EHealthType.HP:
                targetBar = UiManager.Instance.healthHud.HP_Bar;
                targetDecBar = UiManager.Instance.healthHud.HP_Dec_Bar;
                targetRatio = CalculateRatio(_healthPoint, _maxHealthPoint);
                break;
            case EHealthType.MP:
                targetBar = UiManager.Instance.healthHud.MP_Bar;
                targetDecBar = UiManager.Instance.healthHud.MP_Dec_Bar;
                targetRatio = CalculateRatio(_manaPoint, _maxManaPoint);
                break;
            case EHealthType.SP:
                targetBar = UiManager.Instance.healthHud.SP_Bar;
                targetDecBar = UiManager.Instance.healthHud.SP_Dec_Bar;
                targetRatio = CalculateRatio(_staminaPoint, _maxStaminaPoint);
                break;
        }

        if (targetBar == null)
            return;
        if (targetDecBar == null)
            return;

        if(isLerp)
        {
            UiManager.Instance.healthHud.ChangeProgressImmediate(targetBar, targetRatio);
            //UiManager.Instance.healthHud.ChangeProgressImmediate(targetDecBar, targetRatio);
            UiManager.Instance.healthHud.ChangeProgressLerp(targetDecBar, targetRatio);
        }
        else
        {
            UiManager.Instance.healthHud.ChangeProgressImmediate(targetBar, targetRatio);
            UiManager.Instance.healthHud.ChangeProgressImmediate(targetDecBar, targetRatio);
        }
    }

    #endregion  // Damage, Consumption
    public float CalculateRatio(float currentValue, float maxValue)
    {
        return currentValue / maxValue;
    }

    public void InitializeHealthSystem(PlayerStatus playerStatus)
    {
        // ��� ���� �߰� ���ɼ� ����
        _maxHealthPoint = DataManager.Instance.statusLevelData[playerStatus.Vigor].hp;
        _maxManaPoint = DataManager.Instance.statusLevelData[playerStatus.Attunement].mp;
        _maxStaminaPoint = DataManager.Instance.statusLevelData[playerStatus.Endurance].st;

        _staminaPoint = _maxStaminaPoint;
        onChangedHealth(EHealthType.SP, false);
        // ������ ���� �� PlayerStatus ��ġ�� ���� Data Table �����Ͽ� HealthSystem �ʱ�ȭ
    }
    public bool IsAvailableAction()
    {
        return _staminaPoint >= 1f;
    }

    public bool IsDead()
    {
        return _healthPoint <= 0f;
    }
}
