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

    // PlayerStatus�� ��ȭ�� ���� �� ���� �����Ű�� * ������ ����
    public void InitializeCombatStatus(PlayerStatus playerStatus)
    {
        _attackPoint = DataManager.Instance.statusLevelData[playerStatus.AppliedStrength].damage;
        _defensePoint = DataManager.Instance.statusLevelData[playerStatus.AppliedVitality].defense;
        _damageMultiplier = DataManager.Instance.statusLevelData[playerStatus.AppliedDexterity].damageMultiplier;
        // ������ ���� �� PlayerStatus ��ġ�� ���� Data Table �����Ͽ� CombatStatus �ʱ�ȭ
    }
}
