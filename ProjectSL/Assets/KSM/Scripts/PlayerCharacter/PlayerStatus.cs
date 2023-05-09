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

    [SerializeField] int _adder_Equipment_Vigor;
    [SerializeField] int _adder_Equipment_Attunement;
    [SerializeField] int _adder_Equipment_Endurance;
    [SerializeField] int _adder_Equipment_Vitality;
    [SerializeField] int _adder_Equipment_Strength;
    [SerializeField] int _adder_Equipment_Dexterity;

    [SerializeField] int _applied_Vigor;
    [SerializeField] int _applied_Attunement;
    [SerializeField] int _applied_Endurance;
    [SerializeField] int _applied_Vitality;
    [SerializeField] int _applied_Strength;
    [SerializeField] int _applied_Dexterity;

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

        _applied_Vigor= 10;
        _applied_Attunement= 10;
        _applied_Endurance= 10;
        _applied_Vitality= 10;
        _applied_Strength= 10;
        _applied_Dexterity= 10;

        //InitializeAdderValue();
    }
    public void AdderEquipmentValueInit(EquipmentController equipmentController)
    {
        InitializeAdderValue();

        List<ItemData> currentEquipmentList = new List<ItemData>();


        if(equipmentController.ItemData_Helmet != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Helmet);
        }
        if(equipmentController.ItemData_Chest != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Chest);
        }
        if(equipmentController.ItemData_Glove != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Glove);
        }
        if(equipmentController.ItemData_Pants != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Pants);
        }
        if(equipmentController.ItemData_Ring_1 != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Ring_1);
        }
        if(equipmentController.ItemData_Ring_2 != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Ring_2);
        }
        if (equipmentController.ItemData_Ring_3 != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Ring_3);
        }
        if (equipmentController.ItemData_Ring_4 != null)
        {
            currentEquipmentList.Add(equipmentController.ItemData_Ring_4);
        }

        foreach(var equipmentItemData in currentEquipmentList)
        {
            _adder_Equipment_Vigor += equipmentItemData.vigor;
            _adder_Equipment_Attunement += equipmentItemData.attunement;
            _adder_Equipment_Endurance += equipmentItemData.endurance;
            _adder_Equipment_Vitality += equipmentItemData.vitality;
            _adder_Equipment_Strength += equipmentItemData.strength;
            _adder_Equipment_Dexterity += equipmentItemData.dexterity;
            Debug.Log($"adderVital : {_adder_Equipment_Vitality}, EquipItemVital : {equipmentItemData.vitality}");
        }

        CalculateAppliedValue();
    }

    private void InitializeAdderValue()
    {
        _adder_Equipment_Vigor = 0;
        _adder_Equipment_Attunement = 0;
        _adder_Equipment_Endurance = 0;
        _adder_Equipment_Vitality = 0;
        _adder_Equipment_Strength = 0;
        _adder_Equipment_Dexterity = 0;
    }
    private void CalculateAppliedValue()
    {
        _applied_Vigor = _vigor + _adder_Equipment_Vigor;
        _applied_Attunement = _attunement + _adder_Equipment_Attunement;
        _applied_Endurance = _endurance + _adder_Equipment_Endurance;
        _applied_Vitality = _vitality + _adder_Equipment_Vitality;
        _applied_Strength = _strength + _adder_Equipment_Strength;
        _applied_Dexterity = _dexterity + _adder_Equipment_Dexterity;
        Debug.Log($"Applied Vital : {_applied_Vitality} = {_vitality} + {_adder_Equipment_Vitality}");
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

    // applied value property
    public int AppliedVigor { get { return _applied_Vigor; } }
    public int AppliedAttunement { get { return _applied_Attunement; } }
    public int AppliedEndurance { get { return _applied_Endurance; } }
    public int AppliedVitality { get { return _applied_Vitality; } }
    public int AppliedStrength { get { return _applied_Strength; } }
    public int AppliedDexterity { get { return _applied_Dexterity; } }
}

[Serializable]
public class StatusSaveData
{
    [SerializeField] public PlayerStatus _playerStatusData;
    [SerializeField] public HealthSystem _healthSystemData;
    [SerializeField] public float _currentHealthPoint;
    [SerializeField] public float _currentManaPoint;
    [SerializeField] public Vector3 _playerPos;
    [SerializeField] public bool _isPlayerDead;

    public StatusSaveData(PlayerStatus playerStatusData, HealthSystem healthSystem, Vector3 currentPlayerPos)
    {
        _playerStatusData = playerStatusData;
        _healthSystemData = healthSystem;
        _currentHealthPoint = healthSystem.HP;
        _currentManaPoint = healthSystem.MP;
        _playerPos = currentPlayerPos;
        _isPlayerDead = healthSystem.IsDead();
    }
} // StatusSaveData