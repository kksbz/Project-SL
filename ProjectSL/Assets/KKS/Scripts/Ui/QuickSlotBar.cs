using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private QuickSlot leftArm; // 왼손 퀵슬롯
    [SerializeField] private QuickSlot rightArm; // 오른손 퀵슬롯
    [SerializeField] private QuickSlot attackC; // 공격용 소모품 퀵슬롯
    [SerializeField] private QuickSlot recoveryC; // 회복용 소모품 퀵슬롯
    [SerializeField] private List<WeaponSlot> leftWeaponList; // 왼손 무기 리스트
    [SerializeField] private List<WeaponSlot> rightWeaponList; // 오른손 무기 리스트
    [SerializeField] private List<ConsumptionSlot> attackC_List; // 공격용 소모품 리스트
    [SerializeField] private List<ConsumptionSlot> recoveryC_List; // 회복용 소모품 리스트
    public int leftArmNum = 0;
    public int rightArmNum = 0;
    public int attackC_Num = 0;
    public int recoveryC_Num = 0;

    // { Property
    public ItemData QuickSlotRightWeapon { get { return rightArm.Item; } }
    public ItemData QuickSlotLeftWeapon { get { return leftArm.Item; } }
    // } Property
    public GameObject GetCurrentRightWeaponObject { get { return rightWeaponList[rightArmNum].equipItem; } }
    public GameObject GetCurrentLeftWeaponObject { get { return leftWeaponList[leftArmNum].equipItem; } }

    public List<WeaponSlot> LeftWeaponList { get { return leftWeaponList; } }
    public List<WeaponSlot> RightWeaponList { get { return rightWeaponList; } }
    //

    // Start is called before the first frame update
    void Start()
    {
        rightWeaponList = Inventory.Instance.weaponSlotList.GetRange(0, 3);
        leftWeaponList = Inventory.Instance.weaponSlotList.GetRange(3, 3);
        attackC_List = Inventory.Instance.consumptionSlotList.GetRange(0, 3);
        recoveryC_List = Inventory.Instance.consumptionSlotList.GetRange(3, 3);
        leftArm.Item = null;
        rightArm.Item = null;
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
        if (GameManager.Instance.CheckActiveTitleScene() == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                // 마우스 휠을 위로 스크롤했을 때 오른손 무기 장착
                if (scroll > 0f)
                {
                    if (rightWeaponList[rightArmNum].Item != null)
                    {
                        // 이전 슬롯의 아이템 오브젝트 비활성화
                        rightWeaponList[rightArmNum].equipItem.SetActive(false);
                    }
                    // 슬롯의 크기를 벗어나면 인덱스 초기화
                    if (rightArmNum == rightWeaponList.Count - 1)
                    {
                        rightArmNum = -1;
                    }
                    rightArmNum++;
                    rightArm.Item = rightWeaponList[rightArmNum].Item;

                    // 퀵슬롯에 무기가 있을 때
                    if (rightWeaponList[rightArmNum].Item != null)
                    {
                        // 무기 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                        rightWeaponList[rightArmNum].equipItem.SetActive(true);
                        //Debug.Log($"쉬프트+휠업 : {rightArmNum}");
                    }
                }
                // 마우스 휠을 아래로 스크롤했을 때 왼손 무기 장착
                else if (scroll < 0f)
                {
                    if (leftWeaponList[leftArmNum].Item != null)
                    {
                        // 이전 슬롯의 아이템 오브젝트 비활성화
                        leftWeaponList[leftArmNum].equipItem.SetActive(false);
                    }
                    // 슬롯의 크기를 벗어나면 인덱스 초기화
                    if (leftArmNum == leftWeaponList.Count - 1)
                    {
                        leftArmNum = -1;
                    }
                    leftArmNum++;
                    leftArm.Item = leftWeaponList[leftArmNum].Item;

                    // 퀵슬롯에 무기가 있을 때
                    if (leftWeaponList[leftArmNum].Item != null)
                    {
                        // 무기 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                        leftWeaponList[leftArmNum].equipItem.SetActive(true);
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
                    if (attackC_List[attackC_Num].Item != null)
                    {
                        // 이전 슬롯의 아이템 오브젝트 비활성화
                        attackC_List[attackC_Num].equipItem.SetActive(false);
                    }
                    // 슬롯의 크기를 벗어나면 인덱스 초기화
                    if (attackC_Num == attackC_List.Count - 1)
                    {
                        attackC_Num = -1;
                    }
                    attackC_Num++;
                    attackC.Item = attackC_List[attackC_Num].Item;

                    // 퀵슬롯에 공격용 소모품이 있을 때
                    if (attackC_List[attackC_Num].Item != null)
                    {
                        // 공격용 소모품 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                    }
                }
                // 마우스 휠을 아래로 스크롤했을 때 회복용 소모품 장착
                else if (scroll < 0f)
                {
                    if (recoveryC_List[recoveryC_Num].Item != null)
                    {
                        // 이전 슬롯의 아이템 오브젝트 비활성화
                        recoveryC_List[recoveryC_Num].equipItem.SetActive(false);
                    }
                    // 슬롯의 크기를 벗어나면 인덱스 초기화
                    if (recoveryC_Num == recoveryC_List.Count - 1)
                    {
                        recoveryC_Num = -1;
                    }
                    recoveryC_Num++;
                    recoveryC.Item = recoveryC_List[recoveryC_Num].Item;

                    // 퀵슬롯에 회복용 소모품이 있을 때
                    if (recoveryC_List[recoveryC_Num].Item != null)
                    {
                        // 회복용 소모품 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                    }
                }
            }
        }
    } // InPutQuickSlot

    //! 데이터 로드시 퀵슬롯 아이템 갱신하는 함수
    public void LoadQuickSlotData()
    {
        // 오른손 무기 퀵슬롯 갱신
        if (rightWeaponList[rightArmNum].Item != null && rightWeaponList[rightArmNum].equipItem != null)
        {
            rightArm.Item = rightWeaponList[rightArmNum].Item;
            rightWeaponList[rightArmNum].equipItem.SetActive(true);
        }
        else
        {
            rightArm.Item = null;
        }

        // 왼손 무기 퀵슬롯 갱신
        if (leftWeaponList[leftArmNum].Item != null && leftWeaponList[leftArmNum].equipItem != null)
        {
            leftArm.Item = leftWeaponList[leftArmNum].Item;
            leftWeaponList[leftArmNum].equipItem.SetActive(true);
        }
        else
        {
            leftArm.Item = null;
        }

        // 공격용 소모품 퀵슬롯 갱신
        if (attackC_List[attackC_Num].Item != null && attackC_List[attackC_Num].equipItem != null)
        {
            attackC.Item = attackC_List[attackC_Num].Item;
        }
        else
        {
            attackC.Item = null;
        }

        // 회복용 소모품 퀵슬롯 갱신
        if (recoveryC_List[recoveryC_Num].Item != null && recoveryC_List[recoveryC_Num].equipItem != null)
        {
            recoveryC.Item = recoveryC_List[recoveryC_Num].Item;
        }
        else
        {
            recoveryC.Item = null;
        }
        Inventory.Instance._onEquipSlotUpdated();
    } // LoadQuickSlotData
} // QuickSlotBar
