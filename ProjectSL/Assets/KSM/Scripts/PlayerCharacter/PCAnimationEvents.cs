using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private CombatController combatController;
    [SerializeField]
    private Transform playerObjTR;
    [SerializeField]
    private Transform meshObjTR;
    private void Start()
    {
        meshObjTR = transform;
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
    public void OnPCRepositioning()
    {
        Debug.Log("Player Character Repositioning");
        playerObjTR.position = meshObjTR.position;
        meshObjTR.localPosition = Vector3.zero;
    }
    public void On_TempAttackCheck()
    {
        combatController.AttackCheck();
    }
}
