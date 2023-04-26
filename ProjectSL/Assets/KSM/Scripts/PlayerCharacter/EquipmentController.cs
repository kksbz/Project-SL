using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EArmState
{
    NONE = -1,
    OneHanded,
    TwoHanded
}
public class EquipmentController : MonoBehaviour
{
    // ������Ʈ
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CombatController _combatController;

    #region Equipment Item

    // ����
    [Header("���� ���")]
    [SerializeField]
    ItemData _currentRightArmWeapon;
    [SerializeField]
    ItemData _currentLeftArmWeapon;

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

    #endregion  // Equipment Item Data

    EArmState _currentArmState = EArmState.OneHanded;

    #region Weapon Socket
    // Dictionary<string, Transform> _dic_WeaponSockets = new Dictionary<string, Transform>();
    [SerializeField]
    private Transform _rightArmSocket;
    [SerializeField]
    private Transform _leftArmSocket;

    public static readonly string WEAPONSOCKET_RIGHT_ARM    = "RightArmWeaponSocket";
    public static readonly string WEAPONSOCKET_LEFT_ARM     = "LeftArmShieldSocket";

    #endregion  // Weapon Socket

    private void Awake()
    {
        _animator = gameObject.FindChildObj("Mesh").GetComponent<Animator>();
        _combatController = GetComponent<CombatController>();

        _rightArmSocket = gameObject.FindChildObj(WEAPONSOCKET_RIGHT_ARM).transform;
        _leftArmSocket = gameObject.FindChildObj(WEAPONSOCKET_LEFT_ARM).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        // �ӽ� ����
        AttachRightWeaponObj();
        AttachLeftWeaponObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchArmState()
    {

    }

    
    // ������ ������ ��� �˻� �� ���Ͽ� �ڽ����� �ٿ��ֱ� * �ӽ�
    public void AttachRightWeaponObj()
    {
        if(_defaultRightWeaponPrefab != null)
        {
            _defaultRightWeaponPrefab.transform.SetParent(_rightArmSocket);
            _defaultRightWeaponPrefab.transform.localPosition = Vector3.zero;
            _defaultRightWeaponPrefab.transform.localRotation = Quaternion.identity;
        }
    }
    // ������ �޼� ��� �˻� �� ���Ͽ� �ڽ����� �ٿ��ֱ� * �ӽ�
    public void AttachLeftWeaponObj()
    {
        if (_defaultLeftShieldPrefab != null)
        {
            _defaultLeftShieldPrefab.transform.SetParent(_leftArmSocket);
            _defaultLeftShieldPrefab.transform.localPosition = Vector3.zero;
            _defaultLeftShieldPrefab.transform.localRotation = Quaternion.identity;
        }
    }
    


}
