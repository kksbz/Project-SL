using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private GameObject playerLeftArm; // ���Ⱑ ������ �÷��̾��� �޼� ��ġ
    [SerializeField] private GameObject playerRightArm; // ���Ⱑ ������ �÷��̾��� ������ ��ġ
    [SerializeField] private QuickSlot LeftArm; // �޼� ������
    [SerializeField] private QuickSlot RightArm; // ������ ������
    [SerializeField] private QuickSlot consumption; // �Ҹ�ǰ ������
    [SerializeField] private QuickSlot spell; // ���� ������
    [SerializeField] private List<WeaponSlot> LeftWeaponList; // �޼� ���� ����Ʈ
    [SerializeField] private List<WeaponSlot> RightWeaponList; // ������ ���� ����Ʈ
    [SerializeField] private List<ConsumptionSlot> consumptionList; // �Ҹ�ǰ ����Ʈ
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
            // ���콺 ���� ���� ��ũ������ �� ������ ���� ����
            if (scroll > 0f)
            {
                if (rightArmNum != -1)
                {
                    if (RightWeaponList[rightArmNum].Item != null)
                    {
                        // �� ������ ������ ������Ʈ ��Ȱ��ȭ
                        RightWeaponList[rightArmNum].equipItem.SetActive(false);
                    }
                }
                // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                if (rightArmNum == RightWeaponList.Count - 1)
                {
                    rightArmNum = -1;
                }
                rightArmNum++;
                RightArm.Item = RightWeaponList[rightArmNum].Item;

                // �����Կ� ���Ⱑ ���� ��
                if (RightWeaponList[rightArmNum].Item != null)
                {
                    // ���� ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                    RightWeaponList[rightArmNum].equipItem.transform.parent = playerRightArm.transform;
                    RightWeaponList[rightArmNum].equipItem.transform.localPosition = Vector3.zero;
                    RightWeaponList[rightArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    RightWeaponList[rightArmNum].equipItem.SetActive(true);
                    //Debug.Log($"����Ʈ+�پ� : {rightArmNum}");
                }
            }
            // ���콺 ���� �Ʒ��� ��ũ������ �� �޼� ���� ����
            else if (scroll < 0f)
            {
                if (leftArmNum != -1)
                {
                    if (LeftWeaponList[leftArmNum].Item != null)
                    {
                        // �� ������ ������ ������Ʈ ��Ȱ��ȭ
                        LeftWeaponList[leftArmNum].equipItem.SetActive(false);
                    }
                }
                // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                if (leftArmNum == LeftWeaponList.Count - 1)
                {
                    leftArmNum = -1;
                }
                leftArmNum++;
                LeftArm.Item = LeftWeaponList[leftArmNum].Item;

                // �����Կ� ���Ⱑ ���� ��
                if (LeftWeaponList[leftArmNum].Item != null)
                {
                    // ���� ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                    LeftWeaponList[leftArmNum].equipItem.transform.parent = playerLeftArm.transform;
                    LeftWeaponList[leftArmNum].equipItem.transform.localPosition = Vector3.zero;
                    LeftWeaponList[leftArmNum].equipItem.transform.localRotation = Quaternion.identity;
                    LeftWeaponList[leftArmNum].equipItem.SetActive(true);
                    //Debug.Log($"����Ʈ+�ٴٿ� : {leftArmNum}");
                }
            }
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // ���콺 ���� ���� ��ũ������ �� ���� ����
            if (scroll > 0f)
            {
            }
            // ���콺 ���� �Ʒ��� ��ũ������ �� �Ҹ�ǰ ����
            else if (scroll < 0f)
            {
                if (consumptionNum != -1)
                {
                    if (consumptionList[consumptionNum].Item != null)
                    {
                        // �� ������ ������ ������Ʈ ��Ȱ��ȭ
                        consumptionList[consumptionNum].equipItem.SetActive(false);
                    }
                }
                // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                if (consumptionNum == consumptionList.Count - 1)
                {
                    consumptionNum = -1;
                }
                consumptionNum++;
                consumption.Item = consumptionList[consumptionNum].Item;

                // �����Կ� �Ҹ�ǰ�� ���� ��
                if (consumptionList[consumptionNum].Item != null)
                {
                    // �Ҹ�ǰ ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                    consumptionList[consumptionNum].equipItem.transform.parent = playerRightArm.transform;
                    consumptionList[consumptionNum].equipItem.transform.localPosition = Vector3.zero;
                    consumptionList[consumptionNum].equipItem.transform.localRotation = Quaternion.identity;
                    consumptionList[consumptionNum].equipItem.SetActive(true);
                }
            }
        }
    } // InPutQuickSlot
} // QuickSlotBar
