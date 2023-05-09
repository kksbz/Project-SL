using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private CombatController _combatController;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private PlayerStateMachine _playerStateMachine;

    private Animator _animator;

    //private AnimationEventDispatcher _animationEventDispatcher;
    
    private void Awake()
    {
        //_animationEventDispatcher = GetComponent<AnimationEventDispatcher>();
        _animator = GetComponent<Animator>();
        _rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        _playerController = transform.parent.gameObject.GetComponent<PlayerController>();
        _playerStateMachine = transform.parent.gameObject.GetComponent<PlayerStateMachine>();
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
        _rigidbody.velocity = velocity;
        //_characterController.SimpleMove(velocity);
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
    public void OnConsumRecoveryItem()
    {
        _playerController.ConsumRecoveryItem();
    }
    public void EnableRightDamageCollider()
    {
        if (_combatController._currentRightWeaponCollider == null)
            return;

        _combatController._currentRightWeaponCollider.EnableDamageCollider();
    }
    public void DisableRightDamageCollider()
    {
        if (_combatController._currentRightWeaponCollider == null)
            return;

        _combatController._currentRightWeaponCollider.DisableDamageCollider();
    }
    public void EnableLeftDamageCollider()
    {
        if (_combatController._currentLeftWeaponCollider == null)
            return;

        _combatController._currentLeftWeaponCollider.EnableDamageCollider();
    }
    public void DisableLeftDamageCollider()
    {
        if (_combatController._currentLeftWeaponCollider == null)
            return;

        _combatController._currentLeftWeaponCollider.DisableDamageCollider();
    }
    public void OnRotateDuringAttack()
    {
        _playerStateMachine.SetDirectionByAttack();
    }
}
