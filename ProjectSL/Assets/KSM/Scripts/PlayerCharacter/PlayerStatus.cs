using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] int _vigor;
    [SerializeField] int _attunement;
    [SerializeField] int _endurance;
    [SerializeField] int _vitality;
    [SerializeField] int _strength;
    [SerializeField] int _dexterity;
    
    public PlayerStatus()
    {
        _name = string.Empty;
        _level = 1;
        _vigor = 10;
        _attunement = 10;
        _endurance = 10;
        _vitality = 10;
        _strength = 10;
        _dexterity = 10;
    }
    // property
    public string Name { get { return _name; } set { _name = value; } }
    public int Level { get { return _level; } set { _level = value; } }
    public int Vigor { get { return _vigor; } set { _vigor = value; } }
    public int Attunement { get { return _attunement; } set { _attunement = value; } }
    public int Endurance { get { return _endurance; } set { _endurance = value; } }
    public int Vitality { get { return _vitality; } set { _vitality = value; } }
    public int Strength { get { return _strength; } set { _strength = value; } }
    public int Dexterity { get { return _dexterity; } set { _dexterity = value; } }
}

[Serializable]
public class StatusSaveData
{
    [SerializeField] public PlayerStatus _playerStatusData;
    [SerializeField] public float _currentHealthPoint;
    [SerializeField] public float _currentManaPoint;
    [SerializeField] public Vector3 _playerPos;
    [SerializeField] public bool _isPlayerDead;

    public StatusSaveData(PlayerStatus playerStatusData, HealthSystem healthSystem, Vector3 currentPlayerPos)
    {
        _playerStatusData = playerStatusData;
        _currentHealthPoint = healthSystem.HP;
        _currentManaPoint = healthSystem.MP;
        _playerPos = currentPlayerPos;
        _isPlayerDead = healthSystem.IsDead();
    }
} // StatusSaveData