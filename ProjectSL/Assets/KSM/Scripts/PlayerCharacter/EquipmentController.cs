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
public enum EWeaponState
{
    NONE = 0,
    Sword_OneHanded
}
public class EquipmentController : MonoBehaviour
{
    // ������Ʈ
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AnimationController _animationController;
    [SerializeField]
    private CombatController _combatController;
    

    #region Equipment Model Change
    [Header("Equipment Model Changer")]
    HelmetModelChanger _helmetModelChanger;
    ChestModelChanger _chestModelChanger;
    GloveModelChanger _gloveModelChanger;
    PantModelChanger _pantModelChanger;

    SkinnedMeshRenderer _mannequinMesh;
    SkinnedMeshRenderer _helmetMesh;
    SkinnedMeshRenderer _chestMesh;
    SkinnedMeshRenderer _gloveMesh;
    SkinnedMeshRenderer _pantMesh;

    GameObject _helmets;
    GameObject _chests;
    GameObject _gloves;
    GameObject _pants;

    #endregion  // Equipment Model Change

    #region Equipment Item

    // ����
    [Header("���� ���")]
    [SerializeField]
    ItemData _currentRightArmWeapon = default;
    [SerializeField]
    GameObject _currentRightArmWeaponObj = default;
    [SerializeField]
    ItemData _currentLeftArmWeapon = default;
    [SerializeField]
    GameObject _currentLeftArmWeaponObj = default;

    // ����
    [SerializeField]
    ItemData[] _rightWeapon = new ItemData[3];
    [SerializeField]
    ItemData[] _leftWeapon = new ItemData[3];
    // ��
    [SerializeField]
    ItemData _armor_Helmet;
    [SerializeField]
    ItemData _armor_Chest;
    [SerializeField]
    ItemData _armor_Gloves;
    [SerializeField]
    ItemData _armor_Pants;


    // ����
    [SerializeField]
    ItemData _ring_1;
    [SerializeField]
    ItemData _ring_2;
    [SerializeField]
    ItemData _ring_3;
    [SerializeField]
    ItemData _ring_4;

    // �ӽ� �⺻ ���
    [SerializeField]
    private GameObject _defaultRightWeaponPrefab;
    [SerializeField]
    private GameObject _defaultLeftShieldPrefab;

    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private QuickSlotBar _quickSlotBar;

    #endregion  // Equipment Item Data

    EArmState _currentArmState = EArmState.OneHanded;
    EWeaponState _currentWeaponState = EWeaponState.NONE;

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

    public static readonly string WEAPONSOCKET_RIGHT_ARM = "RightArmWeaponSocket";
    public static readonly string WEAPONSOCKET_LEFT_ARM = "LeftArmShieldSocket";
    public static readonly string WEAPONSOCKET_BACK_RIGHTWP = "BackRightWeaponSocket";
    public static readonly string WEAPONSOCKET_BACK_LEFTWP = "BackLeftShieldSocket";

    #endregion  // Weapon Socket

    #region Override Controller

    [SerializeField]
    private RuntimeAnimatorController _default_AnimController;
    // private AnimatorController _default_AnimOV;
    public AnimatorOverrideController _SSH_2H_AnimOV;
    #endregion  // Override Controller

    public delegate void EventHandler();
    public EventHandler _onChangedEquipment;
    public EventHandler _onSwitchiedArmState;

    // Property
    public EArmState ArmState { get { return _currentArmState; } }
    public EWeaponState WeaponState { get { return _currentWeaponState; } }
    public bool IsEquipShieldToLeftArm() 
    {
        if (_currentLeftArmWeapon == null)
            return false;

        return _currentLeftArmWeapon.itemType == ItemData.ItemType.SHIELD; 
    }
    // Property


    private void Awake()
    {
        // Component Init
        GameObject meshObj = gameObject.FindChildObj("Mesh");
        _animator = meshObj.GetComponent<Animator>();
        _combatController = GetComponent<CombatController>();
        _animationController = GetComponent<AnimationController>();

        //_helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
        //_chestModelChanger = GetComponentInChildren<ChestModelChanger>();
        //_gloveModelChanger = GetComponentInChildren<GloveModelChanger>();
        //_pantModelChanger = GetComponentInChildren<PantModelChanger>();


        _rightArmSocket = gameObject.FindChildObj(WEAPONSOCKET_RIGHT_ARM).transform;
        _leftArmSocket = gameObject.FindChildObj(WEAPONSOCKET_LEFT_ARM).transform;
        _backRWSocket = gameObject.FindChildObj(WEAPONSOCKET_BACK_RIGHTWP).transform;
        _backLWSocket = gameObject.FindChildObj(WEAPONSOCKET_BACK_LEFTWP).transform;



        // caching inventory 
        GameObject managers = GFunc.GetRootObj("Managers");
        //GameObject invenObj = managers.FindChildObj("Inventory");
        //_inventory = invenObj.GetComponent<Inventory>();
        //GameObject quickSlotObj = managers.FindChildObj("QuickSlot");
        //_quickSlotBar = quickSlotObj.GetComponent<QuickSlotBar>();
        _quickSlotBar = UiManager.Instance.quickSlotBar;

        //
        /*
        _mannequinMesh = meshObj.GetComponent<SkinnedMeshRenderer>();
        _helmets = gameObject.FindChildObj("Male_Helmets");
        _chests = gameObject.FindChildObj("Male_Chests");
        _gloves = gameObject.FindChildObj("Male_Gloves");
        _pants = gameObject.FindChildObj("Male_Pants");

        _chestMesh = _chests.GetComponentInChildren<SkinnedMeshRenderer>();
        */
        //

        // �ִϸ��̼� �������̵� ��Ʈ�ѷ� �̺�Ʈ ���ε�
        AnimationEventDispatcher aed = meshObj.GetComponent<AnimationEventDispatcher>();
        aed.AddAnimationStartEndByAnimOV(_SSH_2H_AnimOV);

    }
    // Start is called before the first frame update
    void Start()
    {
        // �ӽ� ����
        if (_defaultRightWeaponPrefab != null)
            AttachWeaponObj(_defaultRightWeaponPrefab.transform, _rightArmSocket);
        if (_defaultLeftShieldPrefab != null)
            AttachWeaponObj(_defaultLeftShieldPrefab.transform, _leftArmSocket);
        // default overrideController caching
        _default_AnimController = _animator.runtimeAnimatorController as AnimatorController;

        // �κ��丮 ��������Ʈ �Լ� ���ε�
        Inventory.Instance._onEquipSlotUpdated += UpdateEquipmentItem;
        // �ִϸ��̼� �������̵� ��Ʈ�ѷ� ���� �� �̺�Ʈ ���ε�

        // Mannequin Mesh Hide
        // _mannequinMesh.enabled = false;
        
        //
        // EquipAllEquipmentModelOnStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            // TempEquip();
        }
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
        if (_currentArmState == EArmState.OneHanded)
        {
            _currentArmState = EArmState.TwoHanded;
            RuntimeAnimatorController nextAnimatorController = _SSH_2H_AnimOV; ;
            _animationController.SetAnimatorControllerState(_SSH_2H_AnimOV);
            // AttachWeaponObj(_defaultLeftShieldPrefab.transform, _backLWSocket);
            if (_currentLeftArmWeapon != null)
            {
                AttachWeaponObj(_currentLeftArmWeaponObj.transform, _backLWSocket);
            }
        }
        else if (_currentArmState == EArmState.TwoHanded)
        {
            _currentArmState = EArmState.OneHanded;
            RuntimeAnimatorController nextAnimatorController = _animationController.DefaultAnimatorController; ;
            _animationController.SetAnimatorControllerState(nextAnimatorController);
            // AttachWeaponObj(_defaultLeftShieldPrefab.transform, _leftArmSocket);
            if (_currentLeftArmWeapon != null)
            {
                AttachWeaponObj(_currentLeftArmWeaponObj.transform, _leftArmSocket);
            }
        }
        _onSwitchiedArmState();
        // �ִϸ��̼� ���� �� ���� �ð� �ڿ� ���⸦ ���� �´� ���Ͽ� ���̱�
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

    // ������ ������ ��� �˻� �� ���Ͽ� �ڽ����� �ٿ��ֱ� * �ӽ�
    public void AttachWeaponObj(Transform targetTR, Transform attachTR)
    {
        if (targetTR != null)
        {
            Debug.LogWarning("Attach Check!");
            targetTR.SetParent(attachTR);
            targetTR.localPosition = Vector3.zero;
            targetTR.localRotation = Quaternion.identity;
        }
    }

    public void UpdateEquipmentItem()
    {
        Debug.Log("Enter EquipmentController UpdateEquipmentItem()");
        _rightWeapon[0] = Inventory.Instance.weaponSlotList[0].Item;
        _rightWeapon[1] = Inventory.Instance.weaponSlotList[1].Item;
        _rightWeapon[2] = Inventory.Instance.weaponSlotList[2].Item;

        Debug.Log("���!");

        _leftWeapon[0] = Inventory.Instance.weaponSlotList[3].Item;
        _leftWeapon[1] = Inventory.Instance.weaponSlotList[4].Item;
        _leftWeapon[2] = Inventory.Instance.weaponSlotList[5].Item;

        _armor_Helmet = Inventory.Instance.armorSlotList[0].Item;
        _armor_Chest = Inventory.Instance.armorSlotList[1].Item;
        _armor_Gloves = Inventory.Instance.armorSlotList[2].Item;
        _armor_Pants = Inventory.Instance.armorSlotList[3].Item;

        _currentRightArmWeapon = _quickSlotBar.QuickSlotRightWeapon;
        if (_currentRightArmWeapon != null)
        {
            _currentRightArmWeaponObj = _quickSlotBar.GetCurrentRightWeaponObject;
            AttachWeaponObj(_currentRightArmWeaponObj.transform, _rightArmSocket);
        }
        _currentLeftArmWeapon = _quickSlotBar.QuickSlotLeftWeapon;
        if (_currentLeftArmWeapon != null)
        {
            _currentLeftArmWeaponObj = _quickSlotBar.GetCurrentLeftWeaponObject;
            AttachWeaponObj(_currentLeftArmWeaponObj.transform, _leftArmSocket);
        }

        // �ӽ� *�˹ۿ� ������ ������
        if (_currentRightArmWeapon != null)
        {
            SetWeaponState(EWeaponState.Sword_OneHanded);
        }
        else
        {
            SetWeaponState(EWeaponState.NONE);
        }

        SetArmState(EArmState.OneHanded);

        _onChangedEquipment();
        // ���� ���� ������Ʈ?
        //_ring_1 = Inventory.Instance
    }
    private void SetWeaponState(EWeaponState newState)
    {
        _currentWeaponState = newState;
    }
    private void SetArmState(EArmState newState)
    {
        _currentArmState = newState;
    }
    // public void 
}
