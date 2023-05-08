using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _playerCharacter;
    private PlayerController _playerController;
    private EquipmentController _equipmentController;
    private CharacterControlProperty _controlProperty;
    private AnimationController _animationController;
    private AnimationEventDispatcher _animEventDispatcher;

    #region Attack Field

    public DamageCollider _currentRightWeaponCollider;
    public DamageCollider _currentLeftWeaponCollider;

    public LayerMask EnemyMask;
    public float attackRadius;

    public List<AttackSO> combo;

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

    #region Guard Field

    private bool _isGuard = false;

    #endregion  // Guard Field

    #region Hit Field
    bool _isHit;
    string _hitAnimationTag;
    #endregion  // Hit Field

    #region Block Field
    bool _isBlock;
    string _blockAnimationTag;
    #endregion  // Block Field

    #region Attack Data

    [SerializeField]
    List<AttackSO> comboAttack_R_Pist;
    [SerializeField]
    List<AttackSO> comboAttack_L_Pist;
    [SerializeField]
    List<AttackSO> comboAttack_OH_Sword;
    [SerializeField]
    List<AttackSO> comboAttack_TH_Sword;

    #endregion  // Attack Data

    // Property
    public bool IsAttacking { get { return _isAttacking; } }
    public bool IsGuard { get { return _isGuard; } }
    public bool IsDodging { get { return _isDodging; } }
    public bool IsHit { get { return _isHit; } }
    public bool IsBlock { get { return _isBlock; } }
    public string HitAnimationTag { get { return _hitAnimationTag; } set { _hitAnimationTag = value; } }
    public bool IsPlayingRootMotion { get { return _isPlayingRMAnimation; } }

    [SerializeField]
    private Animator _animator;

    public PoseAction nextPA;

    private bool _isPlayingRMAnimation = false;

    private void Awake()
    {
        // GameObject
        GameObject meshObj = gameObject.FindChildObj("Mesh");

        // Component Init
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerController = GetComponent<PlayerController>();
        _animationController = GetComponent<AnimationController>();
        _equipmentController = GetComponent<EquipmentController>();
        _animator = meshObj.GetComponent<Animator>();

        // Legacy
        playerObjTR = gameObject.transform;
        meshObjTR = gameObject.FindChildObj("Mesh").transform;

        BindingComboAttackEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        _animEventDispatcher = _animator.gameObject.GetComponent<AnimationEventDispatcher>();
        _controlProperty = _playerController.controlProperty;
        _maxCombo = combo.Count;

        _equipmentController._onSwitchiedArmState += SetCurrentCombo;
        _equipmentController._onChangedEquipment += SetCurrentCombo;

        // �Լ� ���ε�
        _animEventDispatcher.onAnimationStart.AddListener(StartedRootMotionAnimation);
        _animEventDispatcher.onAnimationEnd.AddListener(EndedRootMotionAnimation);
        _animEventDispatcher.onAnimationEnd.AddListener(EndedTransitionAnimation);

        _animEventDispatcher.onAnimationEnd.AddListener(InitializeAttackProperty);
        _animEventDispatcher.onAnimationEnd.AddListener(InitializeDodgeProperty);
        _animEventDispatcher.onAnimationEnd.AddListener(InitializeHitProperty);
        _animEventDispatcher.onAnimationEnd.AddListener(InitializeBlockProperty);
        // _animEventDispatcher.onAnimationEnd.AddListener(RootMotionRepositioning);

        SetCurrentCombo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    #region Attack

    public void Attack()
    {
        // ���� ��������
        if (!_canAttack)
            return;

        if (!_playerCharacter.HealthSys.IsAvailableAction())
            return;

        AttackLogic();
    }
    void AttackLogic()
    {
        // Combo Attack
        if (_maxCombo > 1 && _isAttacking)
        {
            //if (_currentCombo < 1 || _currentCombo >= _maxCombo)
            //    return;
            if (_canNextCombo)
            {
                isComboInputOn = true;
            }
        }
        // First Attack
        else
        {
            if (_currentCombo != 0)
                return;

            AttackStartComboState();
            AttackAnimationPlay();
            _isAttacking = true;
            _controlProperty.isAttacking = true;
        }
    }
    void AttackStartComboState()
    {
        _canNextCombo = false;
        isComboInputOn = false;
        _isExecuteImmediateNextCombo = false;
        // 무기 콜라이더 Disable
        _currentRightWeaponCollider.DisableDamageCollider();
        _currentLeftWeaponCollider.DisableDamageCollider();

        if (!CheckComboAssert(_currentCombo, 0, _maxCombo))
        {
            _currentCombo = 1;
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
        // damage collider 끄기
        _currentRightWeaponCollider.DisableDamageCollider();
        _currentLeftWeaponCollider.DisableDamageCollider();

        _isAttacking = false;
        _controlProperty.isAttacking = false;
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
        // 스태미너 감소
        _playerCharacter.HealthSys.ConsumSP(combo[_currentCombo-1].staminaCost);

        PoseAction poseAction = new PoseAction(_animator, "Attack", AnimationController.LAYERINDEX_FULLLAYER, 0, combo[_currentCombo - 1].animatorOV);
        nextPA = poseAction;
        // playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.ATTACK);
        poseAction.Execute();
    }
    // 애니메이션 시작 끝 이벤트 바인딩
    void BindingComboAttackEvent()
    {
        BindingComboAttackEventByAttackSO(comboAttack_L_Pist);
        BindingComboAttackEventByAttackSO(comboAttack_R_Pist);
        BindingComboAttackEventByAttackSO(comboAttack_OH_Sword);
        BindingComboAttackEventByAttackSO(comboAttack_TH_Sword);
    }
    void BindingComboAttackEventByAttackSO(List<AttackSO> comboAttack)
    {
        AnimationEventDispatcher aed = gameObject.GetComponentInChildren<AnimationEventDispatcher>();
        for (int i = 0; i < comboAttack.Count; i++)
        {
            aed.AddAnimationStartEndByAnimOV(comboAttack[i].animatorOV);
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
    // Legacy 테스트용 공격 함수
    public void AttackCheck()
    {
        //Debug.LogWarning("Attack Check");
        Vector3 center = transform.position + transform.forward;
        Collider[] hitResults = Physics.OverlapSphere(center, attackRadius, EnemyMask);
        foreach (Collider hitResult in hitResults)
        {
            Debug.LogWarning($"{hitResult.gameObject.name} �¾���");
            GData.IDamageable damageable = hitResult.gameObject.GetComponent<GData.IDamageable>();
            if(damageable != null)
            {
                // 임시 데미지 30 설정
                damageable.TakeDamage(gameObject, 30f);
            }
        }
    }
    public void InitializeAttackProperty(string name)
    {
        if (!IsAttackAnimation(name))
            return;

        ExitAttack();
    }

    #endregion // Attack

    #region Guard

    public void OnGuard(PlayerBaseState prevState)
    {
        if (_isGuard)
            return;

        // 스태미너 회복 리젠율 감소
        _playerCharacter.HealthSys._staminaRegenMultiplier = 0.5f;
        _isGuard = true;
        // animation Set Layer Weight
        GuardOnOffState();


        if (prevState != null && prevState is PlayerBlockState)
        {
            Debug.Log("OnGuard PrevState : BlockState");
            return;
        }
        GuardTransitionState();
        // guard

    }
    public void OffGuard(PlayerBaseState nextState)
    {
        if (!_isGuard)
            return;
        // 스태미너 회복 리젠율 초기화
        _playerCharacter.HealthSys._staminaRegenMultiplier = 1f;

        _isGuard = false;
        // animation Set Layer Weight
        GuardOnOffState();

        if(nextState != null && nextState is PlayerBlockState)
        {
            _animator.SetLayerWeight(AnimationController.LAYERINDEX_GUARDLAYER, 1);
            Debug.Log("OnGuard nextState : BlockState");
            return;
        }

        GuardTransitionState();
    }
    public void GuardOnOffState()
    {
        if(_isGuard)
        {
            _animator.SetLayerWeight(AnimationController.LAYERINDEX_GUARDLAYER, 1);
        }
        else
        {
            _animator.SetLayerWeight(AnimationController.LAYERINDEX_GUARDLAYER, 0);
        }
    }

    public void GuardTransitionState()
    {
        _animator.SetLayerWeight(AnimationController.LAYERINDEX_TRANSITIONLAYER, 1);
        TransitionAnimationPlay();
    }

    void TransitionAnimationPlay()
    {
        string transitionTag = string.Empty;
        if (_isGuard)
            transitionTag = "Transition_Guard_Begin";
        else
            transitionTag = "Transition_Guard_End";
        PoseAction poseAction = new PoseAction(_animator, transitionTag, AnimationController.LAYERINDEX_TRANSITIONLAYER, 0);
        nextPA = poseAction;
        poseAction.Execute();
    }

    #endregion  // Guard

    #region Dodge

    public void Roll()
    {
        if (!_canDodge)
            return;

        // 스태미너 체크
        if (!_playerCharacter.HealthSys.IsAvailableAction())
            return;

        if(!_isDodging)
        {
            DodgeStartState();
            // 스태미너 감소
            _playerCharacter.HealthSys.ConsumSP(_playerController._rollActionCost);
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
            // 스태미너 감소
            _playerCharacter.HealthSys.ConsumSP(_playerController._backStepActionCost);
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

    #region Hit

    public void Hit()
    {
        if (_isHit)
            return;

        if (_isDodging)
            return;

        HitStartState();
        HitAnimationPlay();
        _isHit = true;
    }

    void HitAnimationPlay()
    {
        PoseAction poseAction = new PoseAction(_animator, HitAnimationTag, AnimationController.LAYERINDEX_FULLLAYER, 0);
        nextPA = poseAction;
        poseAction.Execute();
    }
    void HitStartState()
    {
        
    }
    void HitEndState()
    {
        _isHit = false;
    }
    public void InitializeHitProperty(string name)
    {
        if (IsHitAnimation(name))
        {
            HitEndState();
        }
    }

    #endregion  // Hit

    #region Block
    public void Block()
    {
        if (_isBlock)
            return;

        BlockStartState();
        BlockAnimationPlay();
    }

    void BlockAnimationPlay()
    {
        PoseAction poseAction = new PoseAction(_animator, _blockAnimationTag, AnimationController.LAYERINDEX_FULLLAYER, 0);
        nextPA = poseAction;
        poseAction.Execute();
    }
    void BlockStartState()
    {
        _isBlock = true;
        // 스태미너 소비 후
        _playerCharacter.HealthSys.ConsumSP(_playerController._BlockActionCost);
        if(!_playerCharacter.HealthSys.IsAvailableAction())
        {
            _blockAnimationTag = "Block_Break";
        }
        else
        {
            _blockAnimationTag = "Block";
        }
    }

    void BlockEndState()
    {
        _isBlock = false;
    }
    public void InitializeBlockProperty(string name)
    {
        if (IsBlockAnimation(name))
        {
            BlockEndState();
        }
    }
    #endregion  // Block

    #region Set Attack Data

    void SetCurrentCombo()
    {
        if(_equipmentController.ArmState == EArmState.OneHanded)
        {
            switch(_equipmentController.WeaponState)
            {
                case EWeaponState.NONE:
                    combo = comboAttack_R_Pist;
                    break;
                case EWeaponState.Sword_OneHanded:
                    combo = comboAttack_OH_Sword;
                    break;
            }
        }
        else if(_equipmentController.ArmState == EArmState.TwoHanded)
        {
            switch(_equipmentController.WeaponState)
            {
                case EWeaponState.NONE:
                    break;
                case EWeaponState.Sword_OneHanded:
                    combo = comboAttack_TH_Sword;
                    break;
            }
        }
        _maxCombo = combo.Count;
    }

    #endregion  // Set Attack Data
    public void InitializeAllProperty()
    {
        ExitAttack();
        DodgeEndState();
        HitEndState();
        _animationController.InitializeAnimController();
    }
    private bool IsAttackAnimation(string name)
    {
        return name.StartsWith("Attack");
    }
    private bool IsTransitionAnimation(string name)
    {
        return name.StartsWith("Transition");
    }
    private bool IsRollAnimation(string name)
    {
        return name.StartsWith("Roll");
    }
    private bool IsHitAnimation(string name)
    {
        return name.StartsWith("Hit");
    }
    private bool IsBlockAnimation(string name)
    {
        return name.StartsWith("Block");
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
        InitializeAllProperty();
    }
    public void EndedTransitionAnimation(string name)
    {
        if (!IsTransitionAnimation(name))
            return;

        _animator.SetLayerWeight(AnimationController.LAYERINDEX_TRANSITIONLAYER, 0);
    }

    // Legacy Field
    [SerializeField]
    private Transform playerObjTR;
    [SerializeField]
    private Transform meshObjTR;

    /**
     * Legacy RootMotion Code
     * 루트모션 애니메이션 재생후 끝날 때 플레이어 캐릭터 오브젝트의 위치를 메쉬 오브젝트의 위치로 맞춰주는 함수
     * 루트모션 애니메이션때 콜라이더 상호작용 없이 통과되는 이슈가 있었는데 Animator의 RootMotion Option을 
     * OnAnimatorMove 함수를 작성하여 Handled by Script로 변경하여 함수 내에서 움직임을 처리하는 것으로 해결
     */
    public void RootMotionRepositioning()
    {
        if (!_isPlayingRMAnimation)
            return;

        Debug.LogWarning("Playing Root Motion");
        transform.position = meshObjTR.position - meshObjTR.localPosition;
    }
    public void RootMotionRepositioning(string name)
    {
        if (!IsRootMotionAnimation(name))
            return;

        Debug.Log("Root Motion Animation Ended, Player Character Repositioning");
        playerObjTR.position = meshObjTR.position;
        meshObjTR.localPosition = Vector3.zero;

    }


}
