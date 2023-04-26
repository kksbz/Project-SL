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

    #endregion  // Equipment Item Data

    EArmState _currentArmState = EArmState.OneHanded;

    #region Weapon Socket
    // Dictionary<string, Transform> _dic_WeaponSockets = new Dictionary<string, Transform>();
    [SerializeField]
    private Transform _rigthArmSocket;
    [SerializeField]
    private Transform _leftArmSocket;

    public static readonly string WEAPONSOCKET_RIGHT_ARM    = "RightArm";
    public static readonly string WEAPONSOCKET_LEFT_ARM     = "LeftArm";

    #endregion  // Weapon Socket

    private void Awake()
    {
        _rigthArmSocket = gameObject.FindChildObj(WEAPONSOCKET_RIGHT_ARM).transform;
        _leftArmSocket = gameObject.FindChildObj(WEAPONSOCKET_LEFT_ARM).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchArmState()
    {

    }

    /*
    // ������ ������ ��� �˻� �� ���Ͽ� �ڽ����� �ٿ��ֱ�
    public void AttachRightWeaponObj()
    {

    }
    // ������ �޼� ��� �˻� �� ���Ͽ� �ڽ����� �ٿ��ֱ�
    public void AttachLeftWeaponObj()
    {

    }
    */


}
