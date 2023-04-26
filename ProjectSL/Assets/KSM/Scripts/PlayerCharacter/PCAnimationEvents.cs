using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private CombatController _combatController;
    [SerializeField]
    private CharacterController _characterController;

    private Animator _animator;

    //private AnimationEventDispatcher _animationEventDispatcher;
    
    private void Awake()
    {
        //_animationEventDispatcher = GetComponent<AnimationEventDispatcher>();
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        // meshObjTR = transform;
    }
    
    private void OnAnimatorMove()
    {
        if (!_combatController.IsPlayingRootMotion)
            return;

        float delta = Time.deltaTime;
        Vector3 deltaPosition = _animator.deltaPosition;
        deltaPosition.y = 0f;
        Vector3 velocity = deltaPosition / delta;
        _characterController.SimpleMove(velocity);
    }
    public void OnSetCanNextCombo()
    {
        _combatController.Event_SetCanNextCombo();
    }
    public void OnSetCantNextCombo()
    {
        _combatController.Event_SetCantNextCombo();
    }
    public void OnSetExecuteNextCombo()
    {
        _combatController.Event_SetOnExecuteNextCombo();
    }
    public void OnSetOffExecuteNextCombo()
    {
        _combatController.Event_SetOffExecuteNextCombo();
    }
    public void On_TempAttackCheck()
    {
        _combatController.AttackCheck();
    }
}
