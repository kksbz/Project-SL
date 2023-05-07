using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemActionAnimationType
{
    NONE = -1,
    Drink,
    Throwing
}

[CreateAssetMenu(menuName = "Action/ItemAction")]
public class ItemActionSO : ScriptableObject
{
    public EItemActionAnimationType _animationType;
    public bool _isWalkable;
    public bool _isContinuousable;

    [Header("HealthSystem Value")]
    public EHealthChangeType _healthChangeType;
    public EHealthType _healthType;
    public float _healthChangeValue; 
}
