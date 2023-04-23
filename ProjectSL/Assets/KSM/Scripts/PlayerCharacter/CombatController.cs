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

    // Attack 임시
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

    public bool IsAttacking { get { return _isAttacking; } }

    [SerializeField]
    private Animator _animator;

    public PoseAction nextAttack;

    private void Awake()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
        playerController = GetComponent<PlayerController>();

        BindingComboAttackEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animEventDispatcher = _animator.gameObject.GetComponent<AnimationEventDispatcher>();
        controlProperty = playerController.controlProperty;
        _maxCombo = combo.Count;

        // 함수 바인딩
        _animEventDispatcher.onAnimationEnd.AddListener(InitializeAttackProperty);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        NextAttackCheck();
        ExitAttack();
        */
    }
    public void Attack()
    {
        // 공격 가능한지
        if (!_canAttack)
            return;

        AttackLogic();
    }
    void AttackLogic()
    {
        // 연속공격 체크 (콤보)
        if (_isAttacking)
        {
            if(_currentCombo < 1 || _currentCombo >= _maxCombo)
                return;
            if(_canNextCombo)
            {
                isComboInputOn = true;
            }
        }
        // 첫 공격 체크
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
        Debug.Log($"currentCombo = {_currentCombo}");
    }
    void AttackEndComboState()
    {
        _canNextCombo = false;
        _isExecuteImmediateNextCombo = false;
        _currentCombo = 0;
        _isAttacking = false;
        controlProperty.isAttacking = false;
    }
    bool CheckComboAssert(int current, int start, int max)
    {
        bool isValid = true;
        if(current < start)
            isValid = false;
        if(current >= max)
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
        nextAttack = poseAction;
        // playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.ATTACK);
        poseAction.Execute();
    }
    void BindingComboAttackEvent()
    {
        AnimationEventDispatcher aed = gameObject.GetComponentInChildren<AnimationEventDispatcher>();
        for(int i = 0; i < combo.Count; i++)
        {
            for(int j = 0; j < combo[i].animatorOV.animationClips.Length; j++)
            {
                AnimationClip attackClip = combo[i].animatorOV.animationClips[j];
                if(attackClip == null)
                {
                    continue;
                }
                aed.AddStartAnimationEvent(attackClip);
                aed.AddEndAnimationEvent(attackClip);
            }
        }
    }

    // 애니메이션 이벤트
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
        _isExecuteImmediateNextCombo= true;
    }
    public void Event_SetOffExecuteNextCombo()
    {
        _isExecuteImmediateNextCombo = false;
    }
    public void NextAttackCheck()
    {
        if(!isComboInputOn)
        {
            return;
        }
        if(!_isExecuteImmediateNextCombo)
        {
            return;
        }
        AttackStartComboState();
        AttackAnimationPlay();
    }
    public void AttackCheck()
    {
        Debug.LogWarning("Attack Check");
        Vector3 center = transform.position + transform.forward;
        Collider[] hitResults = Physics.OverlapSphere(center, attackRadius, EnemyMask);
        foreach(Collider hitResult in hitResults) 
        {
            Debug.LogWarning($"{hitResult.gameObject.name} 맞았음");

        }
    }
    public void InitializeAttackProperty(string name)
    {
        Debug.Log("InitializeAttackProperty");
        ExitAttack();
    }
}
