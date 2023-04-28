using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private QuickSlot LeftArm; // 왼손 퀵슬롯
    [SerializeField] private QuickSlot RightArm; // 오른손 퀵슬롯
    [SerializeField] private QuickSlot attackC; // 공격용 소모품 퀵슬롯
    [SerializeField] private QuickSlot recoveryC; // 회복용 소모품 퀵슬롯
    [SerializeField] private List<WeaponSlot> LeftWeaponList; // 왼손 무기 리스트
    [SerializeField] private List<WeaponSlot> RightWeaponList; // 오른손 무기 리스트
    [SerializeField] private List<ConsumptionSlot> AttackC_List; // 공격용 소모품 리스트
    [SerializeField] private List<ConsumptionSlot> RecoveryC_List; // 회복용 소모품 리스트
    public int leftArmNum = 0;
    public int rightArmNum = 0;
    public int attackC_Num = 0;
    public int recoveryC_Num = 0;

    // { Property
    public ItemData QuickSlotRightWeapon { get { return RightArm.Item; } }
    public ItemData QuickSlotLeftWeapon { get { return LeftArm.Item; } }
    // } Property
    public GameObject GetCurrentRightWeaponObject { get { return RightWeaponList[rightArmNum].equipItem; } }
    public GameObject GetCurrentLeftWeaponObject { get { return LeftWeaponList[leftArmNum].equipItem; } }
    //

    // Start is called before the first frame update
    void Start()
    {
        RightWeaponList = Inventory.Instance.weaponSlotList.GetRange(0, 3);
        LeftWeaponList = Inventory.Instance.weaponSlotList.GetRange(3, 3);
        AttackC_List = Inventory.Instance.consumptionSlotList.GetRange(0, 3);
        RecoveryC_List = Inventory.Instance.consumptionSlotList.GetRange(3, 3);
        LeftArm.Item = null;
        RightArm.Item = null;
        attackC.Item = null;
        recoveryC.Item = null;
    } // Start

    // Update is called once per frame
    void Update()
    {
        InPutQuickSlot();
    } // Update

    //! 퀵슬롯 사용 커멘드
    private void InPutQuickSlot()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // 마우스 휠을 위로 스크롤했을 때 오른손 무기 장착
            if (scroll > 0f)
            {
                if (RightWeaponList[rightArmNum].Item != null)
                {
                    // 이전 슬롯의 아이템 오브젝트 비활성화
                    RightWeaponList[rightArmNum].equipItem.SetActive(false);
                }
                // 슬롯의 크기를 벗어나면 인덱스 초기화
                if (rightArmNum == RightWeaponList.Count - 1)
                {
                    rightArmNum = -1;
                }
                rightArmNum++;
                RightArm.Item = RightWeaponList[rightArmNum].Item;

                // 퀵슬롯에 무기가 있을 때
                if (RightWeaponList[rightArmNum].Item != null)
                {
                    // 무기 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                    RightWeaponList[rightArmNum].equipItem.SetActive(true);
                    //Debug.Log($"쉬프트+휠업 : {rightArmNum}");
                }
            }
            // 마우스 휠을 아래로 스크롤했을 때 왼손 무기 장착
            else if (scroll < 0f)
            {
                if (LeftWeaponList[leftArmNum].Item != null)
                {
                    // 이전 슬롯의 아이템 오브젝트 비활성화
                    LeftWeaponList[leftArmNum].equipItem.SetActive(false);
                }
                // 슬롯의 크기를 벗어나면 인덱스 초기화
                if (leftArmNum == LeftWeaponList.Count - 1)
                {
                    leftArmNum = -1;
                }
                leftArmNum++;
                LeftArm.Item = LeftWeaponList[leftArmNum].Item;

                // 퀵슬롯에 무기가 있을 때
                if (LeftWeaponList[leftArmNum].Item != null)
                {
                    // 무기 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                    LeftWeaponList[leftArmNum].equipItem.SetActive(true);
                    //Debug.Log($"쉬프트+휠다운 : {leftArmNum}");
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // 마우스 휠을 위로 스크롤했을 때 공격용 소모품 장착
            if (scroll > 0f)
            {
                if (AttackC_List[attackC_Num].Item != null)
                {
                    // 이전 슬롯의 아이템 오브젝트 비활성화
                    AttackC_List[attackC_Num].equipItem.SetActive(false);
                }
                // 슬롯의 크기를 벗어나면 인덱스 초기화
                if (attackC_Num == AttackC_List.Count - 1)
                {
                    attackC_Num = -1;
                }
                attackC_Num++;
                attackC.Item = AttackC_List[attackC_Num].Item;

                // 퀵슬롯에 공격용 소모품이 있을 때
                if (AttackC_List[attackC_Num].Item != null)
                {
                    // 공격용 소모품 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                }
            }
            // 마우스 휠을 아래로 스크롤했을 때 회복용 소모품 장착
            else if (scroll < 0f)
            {
                if (RecoveryC_List[recoveryC_Num].Item != null)
                {
                    // 이전 슬롯의 아이템 오브젝트 비활성화
                    RecoveryC_List[recoveryC_Num].equipItem.SetActive(false);
                }
                // 슬롯의 크기를 벗어나면 인덱스 초기화
                if (recoveryC_Num == RecoveryC_List.Count - 1)
                {
                    recoveryC_Num = -1;
                }
                recoveryC_Num++;
                recoveryC.Item = RecoveryC_List[recoveryC_Num].Item;

                // 퀵슬롯에 회복용 소모품이 있을 때
                if (RecoveryC_List[recoveryC_Num].Item != null)
                {
                    // 회복용 소모품 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                }
            }
        }
    } // InPutQuickSlot

    //! 데이터 로드시 퀵슬롯 아이템 갱신하는 함수
    public void LoadQuickSlotData()
    {
        // 오른손 무기 퀵슬롯 갱신
        if (RightWeaponList[rightArmNum].Item != null && RightWeaponList[rightArmNum].equipItem != null)
        {
            RightArm.Item = RightWeaponList[rightArmNum].Item;
            RightWeaponList[rightArmNum].equipItem.SetActive(true);
        }
        else
        {
            RightArm.Item = null;
        }

        // 왼손 무기 퀵슬롯 갱신
        if (LeftWeaponList[leftArmNum].Item != null && LeftWeaponList[leftArmNum].equipItem != null)
        {
            LeftArm.Item = LeftWeaponList[leftArmNum].Item;
            LeftWeaponList[leftArmNum].equipItem.SetActive(true);
        }
        else
        {
            LeftArm.Item = null;
        }

        // 공격용 소모품 퀵슬롯 갱신
        if (AttackC_List[attackC_Num].Item != null && AttackC_List[attackC_Num].equipItem != null)
        {
            attackC.Item = AttackC_List[attackC_Num].Item;
        }
        else
        {
            attackC.Item = null;
        }

        // 회복용 소모품 퀵슬롯 갱신
        if (RecoveryC_List[recoveryC_Num].Item != null && RecoveryC_List[recoveryC_Num].equipItem != null)
        {
            recoveryC.Item = RecoveryC_List[recoveryC_Num].Item;
        }
        else
        {
            recoveryC.Item = null;
        }
        Inventory.Instance._onEquipSlotUpdated();
    } // LoadQuickSlotData
} // QuickSlotBar
