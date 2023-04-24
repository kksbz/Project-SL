using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter playerCharacter;
    private PlayerController playerController;
    private CharacterControlProperty controlProperty;
    private AnimationEventDispatcher _animEventDispatcher;

    #region Attack Field

    public LayerMask EnemyMask;
    public float attackRadius;

    public List<AttackSO> combo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter = 0;

    public bool _canAttack = true;
    private bool _canNextCombo = default;
    private bool _isExecuteImmediateNextCombo = default;
    private bool _isAttacking = false;
    private bool isComboInputOn = default;
    private int _currentCombo = 0;
    private int _maxCombo = default;

    #endregion // Attack Field

    #region Roll Field

    public bool _canDodge = true;
    private bool _isDodging = false;

    #endregion  // Roll Field

    // Property
    public bool IsAttacking { get { return _isAttacking; } }
    public bool IsDodging { get { return _isDodging; } }

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Transform playerObjTR;
    [SerializeField]
    private Transform meshObjTR;

    public PoseAction nextPA;

    private bool _isPlayingRMAnimation = false;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        playerController = GetComponent<PlayerController>();
        playerObjTR = gameObject.transform;
        meshObjTR = gameObject.FindChildObj("Mesh").transform;

        BindingComboAttackEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animEventDispatcher = _animator.gameObject.GetComponent<AnimationEventDispatcher>();
        controlProperty = playerController.controlProperty;
        _maxCombo = combo.Count;

        // �Լ� ���ε�
        _animEventDispatcher.onAnimationStart.AddListener(StartedRootMotionAnimation);
        _animEventDispatcher.onAnimationEnd.AddListener(EndedRootMotionAnimation);

        _animEventDispatcher.onAnimationEnd.AddListener(InitializeAttackProperty);
        _animEventDispatcher.onAnimationEnd.AddListener(InitializeDodgeProperty);
        _animEventDispatcher.onAnimationEnd.AddListener(RootMotionRepositioning);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning($"_isAttacking : {_isAttacking}");
        /*
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        NextAttackCheck();
        ExitAttack();
        */
    }

    private void FixedUpdate()
    {
        RootMotionRepositioning();
    }

    #region Attack

    public void Attack()
    {
        // ���� ��������
        if (!_canAttack)
            return;

        AttackLogic();
    }
    void AttackLogic()
    {
        // ���Ӱ��� üũ (�޺�)
        if (_isAttacking)
        {
            if (_currentCombo < 1 || _currentCombo >= _maxCombo)
                return;
            if (_canNextCombo)
            {
                isComboInputOn = true;
            }
        }
        // ù ���� üũ
        else
        {
            if (_currentCombo != 0)
                return;
            AttackStartComboState();
            AttackAnimationPlay();
            _isAttacking = true;
            controlProperty.isAttacking = true;
        }
    }
    void AttackStartComboState()
    {
        _canNextCombo = false;
        isComboInputOn = false;
        _isExecuteImmediateNextCombo = false;

        if (!CheckComboAssert(_currentCombo, 0, _maxCombo))
        {
            return;
        }
        _currentCombo = Mathf.Clamp(_currentCombo + 1, 1, _maxCombo);
        //Debug.Log($"currentCombo = {currentCombo}");
    }
    void AttackEndComboState()
    {
        _canNextCombo = false;
        _isExecuteImmediateNextCombo = false;
        _currentCombo = 0;
        Debug.LogWarning("_isAttacking False");
        _isAttacking = false;
        controlProperty.isAttacking = false;
    }
    bool CheckComboAssert(int current, int start, int max)
    {
        bool isValid = true;
        if (current < start)
            isValid = false;
        if (current >= max)
            isValid = false;
        return isValid;
    }
    public void ExitAttack()
    {
        AttackEndComboState();
    }
    void AttackAnimationPlay()
    {
        PoseAction poseAction = new PoseAction(_animator, "Attack", AnimationController.LAYERINDEX_FULLLAYER, 0, combo[_currentCombo - 1].animatorOV);
        nextPA = poseAction;
        // playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.ATTACK);
        poseAction.Execute();
    }
    void BindingComboAttackEvent()
    {
        AnimationEventDispatcher aed = gameObject.GetComponentInChildren<AnimationEventDispatcher>();
        for (int i = 0; i < combo.Count; i++)
        {
            for (int j = 0; j < combo[i].animatorOV.animationClips.Length; j++)
            {
                AnimationClip attackClip = combo[i].animatorOV.animationClips[j];
                if (attackClip == null)
                {
                    continue;
                }
                aed.AddStartAnimationEvent(attackClip);
                aed.AddEndAnimationEvent(attackClip);
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ
    public void Event_SetCanNextCombo()
    {
        _canNextCombo = true;
    }
    public void Event_SetCantNextCombo()
    {
        _canNextCombo = false;
    }
    public void Event_SetOnExecuteNextCombo()
    {
        _isExecuteImmediateNextCombo = true;
    }
    public void Event_SetOffExecuteNextCombo()
    {
        _isExecuteImmediateNextCombo = false;
    }
    public void NextAttackCheck()
    {
        if (!isComboInputOn)
        {
            return;
        }
        if (!_isExecuteImmediateNextCombo)
        {
            return;
        }
        AttackStartComboState();
        AttackAnimationPlay();
    }
    public void AttackCheck()
    {
        //Debug.LogWarning("Attack Check");
        Vector3 center = transform.position + transform.forward;
        Collider[] hitResults = Physics.OverlapSphere(center, attackRadius, EnemyMask);
        foreach (Collider hitResult in hitResults)
        {
            Debug.LogWarning($"{hitResult.gameObject.name} �¾���");

        }
    }
    public void InitializeAttackProperty(string name)
    {
        if (!IsAttackAnimation(name))
            return;

        ExitAttack();
    }

    #endregion // Attack

    #region Dodge

    public void Roll()
    {
        if (!_canDodge)
            return;

        if(!_isDodging)
        {
            DodgeStartState();
            DodgeAnimationPlay("Roll");
        }
    }
    public void BackStep()
    {
        if (!_canDodge)
            return;
        if(!_isDodging)
        {
            DodgeStartState();
            DodgeAnimationPlay("BackStep");
        }
    }

    void DodgeStartState()
    {
        _isDodging = true;
        // ������ ���� ó�� todo?
    }
    void DodgeEndState()
    {
        _isDodging = false;
    }

    void DodgeAnimationPlay(string dodge)
    {
        PoseAction poseAction = new PoseAction(_animator, dodge, AnimationController.LAYERINDEX_FULLLAYER, 0);
        nextPA = poseAction;
        // playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.ATTACK);
        poseAction.Execute();
    }

    public void InitializeDodgeProperty(string name)
    {
        if (IsRollAnimation(name) || IsBackStepAnimation(name))
        {
            DodgeEndState();
        }
    }

    #endregion  // Dodge

    public void RootMotionRepositioning(string name)
    {
        if (!IsRootMotionAnimation(name))
            return;

        Debug.Log("Root Motion Animation Ended, Player Character Repositioning");
        playerObjTR.position = meshObjTR.position;
        meshObjTR.localPosition = Vector3.zero;

    }
    public void RootMotionRepositioning()
    {
        if (!_isPlayingRMAnimation)
            return;

        Debug.LogWarning("Playing Root Motion");
        transform.position = meshObjTR.position - meshObjTR.localPosition;
    }
    private bool IsAttackAnimation(string name)
    {
        return name.StartsWith("Attack");
    }
    private bool IsRollAnimation(string name)
    {
        return name.StartsWith("Roll");
    }
    private bool IsBackStepAnimation(string name)
    {
        return name.StartsWith("BackStep");
    }
    private bool IsRootMotionAnimation(string name)
    {
        return name.EndsWith("Rm");
    }

    public void StartedRootMotionAnimation(string name)
    {
        if (!IsRootMotionAnimation(name))
            return;

        _isPlayingRMAnimation = true;
    }
    public void EndedRootMotionAnimation(string name)
    {
        if (!IsRootMotionAnimation(name))
            return;

        _isPlayingRMAnimation = false;
    }
}
