using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private GameObject playerLeftArm;
    [SerializeField] private GameObject playerRightArm;
    [SerializeField] private QuickSlot LeftArm; // 왼손 이미지
    [SerializeField] private QuickSlot RightArm; // 오른손 이미지
    [SerializeField] private QuickSlot consumption; // 소모품 이미지
    [SerializeField] private QuickSlot spell; // 스펠 이미지
    [SerializeField] private List<WeaponSlot> LeftWeaponList;
    [SerializeField] private List<WeaponSlot> RightWeaponList;
    [SerializeField] private List<ConsumptionSlot> consumptionList;
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
            if (scroll > 0f)
            {
                if (rightArmNum != -1)
                {
                    if (RightWeaponList[rightArmNum].Item != null)
                    {
                        RightWeaponList[rightArmNum].equipItem.SetActive(false);
                    }
                }
                // 마우스 휠을 위로 스크롤했을 때 오른손 무기 장착
                if (rightArmNum == RightWeaponList.Count - 1)
                {
                    rightArmNum = -1;
                }
                rightArmNum++;
                RightArm.Item = RightWeaponList[rightArmNum].Item;
                if (RightWeaponList[rightArmNum].Item != null)
                {
                    RightWeaponList[rightArmNum].equipItem.transform.parent = playerRightArm.transform;
                    RightWeaponList[rightArmNum].equipItem.transform.localPosition = Vector3.zero;
                    RightWeaponList[rightArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    RightWeaponList[rightArmNum].equipItem.SetActive(true);
                    Debug.Log($"쉬프트+휠업 : {rightArmNum}");
                }
            }
            else if (scroll < 0f)
            {
                if (leftArmNum != -1)
                {
                    if (LeftWeaponList[leftArmNum].Item != null)
                    {
                        LeftWeaponList[leftArmNum].equipItem.SetActive(false);
                    }
                }
                // 마우스 휠을 아래로 스크롤했을 때 왼손 무기 장착
                if (leftArmNum == LeftWeaponList.Count - 1)
                {
                    leftArmNum = -1;
                }
                leftArmNum++;
                LeftArm.Item = LeftWeaponList[leftArmNum].Item;
                if (LeftWeaponList[leftArmNum].Item != null)
                {
                    LeftWeaponList[leftArmNum].equipItem.transform.parent = playerLeftArm.transform;
                    LeftWeaponList[leftArmNum].equipItem.transform.localPosition = Vector3.zero;
                    LeftWeaponList[leftArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    LeftWeaponList[leftArmNum].equipItem.SetActive(true);
                    Debug.Log($"쉬프트+휠다운 : {leftArmNum}");
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // 마우스 휠을 위로 스크롤했을 때 스펠 장착
            }
            else if (scroll < 0f)
            {
                if (consumptionNum != -1)
                {
                    if (consumptionList[consumptionNum].Item != null)
                    {
                        consumptionList[consumptionNum].equipItem.SetActive(false);
                    }
                }
                // 마우스 휠을 아래로 스크롤했을 때 소모품 장착
                if (consumptionNum == consumptionList.Count - 1)
                {
                    consumptionNum = -1;
                }
                consumptionNum++;
                consumption.Item = consumptionList[consumptionNum].Item;
                if (consumptionList[consumptionNum].Item != null)
                {
                    consumptionList[consumptionNum].equipItem.transform.parent = playerLeftArm.transform;
                    consumptionList[consumptionNum].equipItem.transform.localPosition = Vector3.zero;
                    consumptionList[consumptionNum].equipItem.transform.localRotation = Quaternion.identity;
                    consumptionList[consumptionNum].equipItem.SetActive(true);
                }
            }
        }
    } // InPutQuickSlot

    //! 퀵슬롯의 아이템을 생성하는 함수
    public void InstantEquipItem(ItemData itemData)
    {
        if (itemData != null || itemData.itemType != ItemData.ItemType.NONE)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{itemData.itemID}"));
            item.SetActive(false);
        }
    } // EquipItem
} // QuickSlotBar
