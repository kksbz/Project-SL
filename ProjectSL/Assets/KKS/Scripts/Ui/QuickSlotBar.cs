using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private GameObject playerLeftArm; // 무기가 장착될 플레이어의 왼손 위치
    [SerializeField] private GameObject playerRightArm; // 무기가 장착될 플레이어의 오른손 위치
    [SerializeField] private QuickSlot LeftArm; // 왼손 퀵슬롯
    [SerializeField] private QuickSlot RightArm; // 오른손 퀵슬롯
    [SerializeField] private QuickSlot consumption; // 소모품 퀵슬롯
    [SerializeField] private QuickSlot spell; // 스펠 퀵슬롯
    [SerializeField] private List<WeaponSlot> LeftWeaponList; // 왼손 무기 리스트
    [SerializeField] private List<WeaponSlot> RightWeaponList; // 오른손 무기 리스트
    [SerializeField] private List<ConsumptionSlot> consumptionList; // 소모품 리스트
    private int leftArmNum = -1;
    private int rightArmNum = -1;
    private int consumptionNum = -1;
    private int spellNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerLeftArm = GameManager.Instance.playerLeftArm;
        playerRightArm = GameManager.Instance.playerRightArm;
        RightWeaponList = Inventory.Instance.weaponSlotList.GetRange(0, 3);
        LeftWeaponList = Inventory.Instance.weaponSlotList.GetRange(3, 3);
        consumptionList = Inventory.Instance.consumptionSlotList;
        LeftArm.Item = null;
        RightArm.Item = null;
        consumption.Item = null;
        spell.Item = null;
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
                if (rightArmNum != -1)
                {
                    if (RightWeaponList[rightArmNum].Item != null)
                    {
                        // 전 슬롯의 아이템 오브젝트 비활성화
                        RightWeaponList[rightArmNum].equipItem.SetActive(false);
                    }
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
                    RightWeaponList[rightArmNum].equipItem.transform.parent = playerRightArm.transform;
                    RightWeaponList[rightArmNum].equipItem.transform.localPosition = Vector3.zero;
                    RightWeaponList[rightArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    RightWeaponList[rightArmNum].equipItem.SetActive(true);
                    //Debug.Log($"쉬프트+휠업 : {rightArmNum}");
                }
            }
            // 마우스 휠을 아래로 스크롤했을 때 왼손 무기 장착
            else if (scroll < 0f)
            {
                if (leftArmNum != -1)
                {
                    if (LeftWeaponList[leftArmNum].Item != null)
                    {
                        // 전 슬롯의 아이템 오브젝트 비활성화
                        LeftWeaponList[leftArmNum].equipItem.SetActive(false);
                    }
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
                    LeftWeaponList[leftArmNum].equipItem.transform.parent = playerLeftArm.transform;
                    LeftWeaponList[leftArmNum].equipItem.transform.localPosition = Vector3.zero;
                    LeftWeaponList[leftArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    LeftWeaponList[leftArmNum].equipItem.SetActive(true);
                    //Debug.Log($"쉬프트+휠다운 : {leftArmNum}");
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // 마우스 휠을 위로 스크롤했을 때 스펠 장착
            if (scroll > 0f)
            {
            }
            // 마우스 휠을 아래로 스크롤했을 때 소모품 장착
            else if (scroll < 0f)
            {
                if (consumptionNum != -1)
                {
                    if (consumptionList[consumptionNum].Item != null)
                    {
                        // 전 슬롯의 아이템 오브젝트 비활성화
                        consumptionList[consumptionNum].equipItem.SetActive(false);
                    }
                }
                // 슬롯의 크기를 벗어나면 인덱스 초기화
                if (consumptionNum == consumptionList.Count - 1)
                {
                    consumptionNum = -1;
                }
                consumptionNum++;
                consumption.Item = consumptionList[consumptionNum].Item;

                // 퀵슬롯에 소모품이 있을 때
                if (consumptionList[consumptionNum].Item != null)
                {
                    // 소모품 오브젝트를 플레이어의 오른손으로 위치 조정하고 활성화
                    consumptionList[consumptionNum].equipItem.transform.parent = playerRightArm.transform;
                    consumptionList[consumptionNum].equipItem.transform.localPosition = Vector3.zero;
                    consumptionList[consumptionNum].equipItem.transform.localRotation = Quaternion.identity;
                    consumptionList[consumptionNum].equipItem.SetActive(true);
                }
            }
        }
    } // InPutQuickSlot
} // QuickSlotBar
