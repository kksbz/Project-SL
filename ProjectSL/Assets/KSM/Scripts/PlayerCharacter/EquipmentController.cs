using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum EArmState
{
    NONE = -1,
    OneHanded,
    TwoHanded
}
public class EquipmentController : MonoBehaviour
{
    // 컴포넌트
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CombatController _combatController;

    #region Equipment Item

    // 무기
    [Header("현재 장비")]
    [SerializeField]
    ItemData _currentRightArmWeapon = default;
    [SerializeField]
    ItemData _currentLeftArmWeapon = default;

    // 무기
    [SerializeField]
    ItemData[] _rightWeapon = new ItemData[3];
    [SerializeField]
    ItemData[] _leftWeapon = new ItemData[3];
    // 방어구
    [SerializeField]
    ItemData _armor_Helmet;
    [SerializeField]
    ItemData _armor_Chest; 
    [SerializeField]
    ItemData _armor_Gloves;
    [SerializeField]
    ItemData _armor_Pants;
    

    // 반지
    [SerializeField]
    ItemData _ring_1;
    [SerializeField]
    ItemData _ring_2;
    [SerializeField]
    ItemData _ring_3;
    [SerializeField]
    ItemData _ring_4;

    // 임시 기본 장비
    [SerializeField]
    private GameObject _defaultRightWeaponPrefab;
    [SerializeField]
    private GameObject _defaultLeftShieldPrefab;

    [SerializeField]
    private Inventory _inventory;

    #endregion  // Equipment Item Data

    EArmState _currentArmState = EArmState.OneHanded;

    bool _isSwitching = false;

    #region Weapon Socket
    // Dictionary<string, Transform> _dic_WeaponSockets = new Dictionary<string, Transform>();
    [SerializeField]
    private Transform _rightArmSocket;
    [SerializeField]
    private Transform _leftArmSocket;
    [SerializeField]
    private Transform _backRWSocket;
    [SerializeField]
    private Transform _backLWSocket;

    public static readonly string WEAPONSOCKET_RIGHT_ARM    = "RightArmWeaponSocket";
    public static readonly string WEAPONSOCKET_LEFT_ARM     = "LeftArmShieldSocket";
    public static readonly string WEAPONSOCKET_BACK_RIGHTWP = "BackRightWeaponSocket";
    public static readonly string WEAPONSOCKET_BACK_LEFTWP  = "BackLeftShieldSocket";

    #endregion  // Weapon Socket

    #region Override Controller
    [SerializeField]
    private AnimatorController _default_AnimController;
    // private AnimatorController _default_AnimOV;
    public AnimatorOverrideController _SSH_2H_AnimOV;
    #endregion  // Override Controller

    private void Awake()
    {
        GameObject meshObj = gameObject.FindChildObj("Mesh");
        _animator = meshObj.GetComponent<Animator>();
        _combatController = GetComponent<CombatController>();

        _rightArmSocket = gameObject.FindChildObj(WEAPONSOCKET_RIGHT_ARM).transform;
        _leftArmSocket = gameObject.FindChildObj(WEAPONSOCKET_LEFT_ARM).transform;
        _backRWSocket = gameObject.FindChildObj(WEAPONSOCKET_BACK_RIGHTWP).transform;
        _backLWSocket = gameObject.FindChildObj(WEAPONSOCKET_BACK_LEFTWP).transform;

        // caching inventory 
        GameObject managers = GFunc.GetRootObj("Managers");
        //GameObject invenObj = managers.FindChildObj("Inventory");
        //_inventory = invenObj.GetComponent<Inventory>();

        // 애니메이션 오버라이드 컨트롤러 이벤트 바인딩
        AnimationEventDispatcher aed = meshObj.GetComponent<AnimationEventDispatcher>();
        aed.AddAnimationStartEndByAnimOV(_SSH_2H_AnimOV);
    }
    // Start is called before the first frame update
    void Start()
    {
        // 임시 부착
        AttachWeaponObj(_defaultRightWeaponPrefab.transform, _rightArmSocket);
        AttachWeaponObj(_defaultLeftShieldPrefab.transform, _leftArmSocket);
        // default overrideController caching
        _default_AnimController =_animator.runtimeAnimatorController as AnimatorController;

        // 애니메이션 오버라이드 컨트롤러 시작 끝 이벤트 바인딩

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchArmState()
    {
        if (_isSwitching)
            return;

        _isSwitching = true;
        string switchingTag = string.Empty;
        if (_currentLeftArmWeapon != null)
        {
            Debug.Log("_currentLeftArmWeapon is not null");
            switchingTag = "Transition_ArmState_LeftArm";
        }
        else
        {
            Debug.Log("_currentLeftArmWeapon is null");
            switchingTag = "Transition_ArmState_RightArm";
        }

        SwitchingArmAnimationPlay(switchingTag);
        StartCoroutine(SwitchingArm());
    }
    IEnumerator SwitchingArm()
    {
        yield return new WaitForSeconds(0.3f);
        if(_currentArmState == EArmState.OneHanded)
        {
            _currentArmState = EArmState.TwoHanded;
            _animator.runtimeAnimatorController = _SSH_2H_AnimOV;
            AttachWeaponObj(_defaultLeftShieldPrefab.transform, _backLWSocket);
        }
        else if(_currentArmState == EArmState.TwoHanded)
        {
            _currentArmState = EArmState.OneHanded;
            _animator.runtimeAnimatorController = _default_AnimController;
            AttachWeaponObj(_defaultLeftShieldPrefab.transform, _leftArmSocket);
        }
        // 애니메이션 실행 후 일정 시간 뒤에 무기를 각각 맞는 소켓에 붙이기
        _isSwitching = false;
    }
    private void SwitchingArmAnimationPlay(string stateTag)
    {
        //PoseAction poseAction = new PoseAction(_animator, "Attack", AnimationController.LAYERINDEX_FULLLAYER, 0, combo[_currentCombo - 1].animatorOV);
        //nextPA = poseAction;
        // playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.ATTACK);
        //poseAction.Execute();
        _animator.SetLayerWeight(AnimationController.LAYERINDEX_TRANSITIONLAYER, 1);
        PoseAction poseAction = new PoseAction(_animator, stateTag, AnimationController.LAYERINDEX_TRANSITIONLAYER);
        poseAction.Execute();
    }
    
    // 퀵슬롯 오른손 장비 검색 후 소켓에 자식으로 붙여주기 * 임시
    public void AttachWeaponObj(Transform targetTR, Transform attachTR)
    {
        if(targetTR != null)
        {
            Debug.LogWarning("Attach Check!");
            targetTR.SetParent(attachTR);
            targetTR.localPosition = Vector3.zero;
            targetTR.localRotation = Quaternion.identity;
        }
    }

    // public void 
}
