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
    // 컴포넌트
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CombatController _combatController;

    #region Equipment Item

    // 무기
    [Header("현재 장비")]
    [SerializeField]
    ItemData _currentRightArmWeapon;
    [SerializeField]
    ItemData _currentLeftArmWeapon;

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
    // 퀵슬롯 오른손 장비 검색 후 소켓에 자식으로 붙여주기
    public void AttachRightWeaponObj()
    {

    }
    // 퀵슬롯 왼손 장비 검색 후 소켓에 자식으로 붙여주기
    public void AttachLeftWeaponObj()
    {

    }
    */


}
