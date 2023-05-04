using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombatStatus
{
    [SerializeField] int _attackPoint;
    [SerializeField] int _defensePoint;
    [SerializeField] float _damageMultiplier;
    
    // Property
    public int AttackPoint { get { return _attackPoint; } }
    public int DefensePoint { get { return _defensePoint; } }
    public float DamageMultiplier { get { return _damageMultiplier; } }
    //
    public CombatStatus()
    {
        _attackPoint = 30;
        _defensePoint = 86;
        _damageMultiplier = 1f;
    }

    // PlayerStatus에 변화가 있을 때 마다 실행시키기 * 레벨업 같은
    public void InitializeCombatStatus(PlayerStatus playerStatus)
    {
        _attackPoint = DataManager.Instance.statusLevelData[playerStatus.Strength].damage;
        _defensePoint = DataManager.Instance.statusLevelData[playerStatus.Vitality].defense;
        _damageMultiplier = DataManager.Instance.statusLevelData[playerStatus.Dexterity].damageMultiplier;
        // 데이터 연동 후 PlayerStatus 수치에 따라 Data Table 참조하여 CombatStatus 초기화
    }
}
