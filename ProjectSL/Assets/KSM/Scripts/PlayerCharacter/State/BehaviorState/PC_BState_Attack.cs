using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_BState_Attack : BehaviorStateBase
{
    private Animator animator;
    private CombatController combatController;
    private PoseAction nextAction;
    private Behavior wait;
    public PC_BState_Attack(PlayerController controller, Animator animator, CombatController combatController) : base(controller)
    {
        this.animator = animator;
        this.combatController = combatController;
        nextAction = default;
        wait = default;
    }
    public override void OnEnterState()
    {
        
        //Debug.Log("Enter Attack State");
        animator.applyRootMotion = true;
        //Debug.Log($"applyRootMotion : {animator.applyRootMotion}");
        nextAction = combatController.nextAttack;
        nextAction.Execute();
        /* Do Nothing */
    }
    public override void OnExitState()
    {
        animator.applyRootMotion = false;
        //Debug.Log("Exit Attack State");
        /* Do Nothing */
    }
    public override void OnUpdateState()
    {
        wait = new Behavior();
    }
    public override void OnFixedUpdateState()
    {
        if(wait != null)
        {
            wait.Execute();
        }
    }
}
