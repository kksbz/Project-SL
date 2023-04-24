using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private CombatController combatController;

    //private AnimationEventDispatcher _animationEventDispatcher;
    
    private void Awake()
    {
        //_animationEventDispatcher = GetComponent<AnimationEventDispatcher>();
    }
    private void Start()
    {
        // meshObjTR = transform;
    }
    public void OnSetCanNextCombo()
    {
        combatController.Event_SetCanNextCombo();
    }
    public void OnSetCantNextCombo()
    {
        combatController.Event_SetCantNextCombo();
    }
    public void OnSetExecuteNextCombo()
    {
        combatController.Event_SetOnExecuteNextCombo();
    }
    public void OnSetOffExecuteNextCombo()
    {
        combatController.Event_SetOffExecuteNextCombo();
    }
    public void On_TempAttackCheck()
    {
        combatController.AttackCheck();
    }
}
