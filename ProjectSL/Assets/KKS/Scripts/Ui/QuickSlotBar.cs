using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private GameObject playerLeftArm;
    [SerializeField] private GameObject playerRightArm;
    [SerializeField] private QuickSlot LeftArm; // �޼� �̹���
    [SerializeField] private QuickSlot RightArm; // ������ �̹���
    [SerializeField] private QuickSlot consumption; // �Ҹ�ǰ �̹���
    [SerializeField] private QuickSlot spell; // ���� �̹���
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

    //! ������ ��� Ŀ���
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
                // ���콺 ���� ���� ��ũ������ �� ������ ���� ����
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
                    Debug.Log($"����Ʈ+�پ� : {rightArmNum}");
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
                // ���콺 ���� �Ʒ��� ��ũ������ �� �޼� ���� ����
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
                    Debug.Log($"����Ʈ+�ٴٿ� : {leftArmNum}");
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // ���콺 ���� ���� ��ũ������ �� ���� ����
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
                // ���콺 ���� �Ʒ��� ��ũ������ �� �Ҹ�ǰ ����
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

    //! �������� �������� �����ϴ� �Լ�
    public void InstantEquipItem(ItemData itemData)
    {
        if (itemData != null || itemData.itemType != ItemData.ItemType.NONE)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{itemData.itemID}"));
            item.SetActive(false);
        }
    } // EquipItem
} // QuickSlotBar
