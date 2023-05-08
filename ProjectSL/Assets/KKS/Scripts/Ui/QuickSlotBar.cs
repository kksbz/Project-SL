using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private QuickSlot leftArm; // �޼� ������
    [SerializeField] private QuickSlot rightArm; // ������ ������
    [SerializeField] private QuickSlot attackC; // ���ݿ� �Ҹ�ǰ ������
    [SerializeField] private QuickSlot recoveryC; // ȸ���� �Ҹ�ǰ ������
    [SerializeField] private List<WeaponSlot> leftWeaponList; // �޼� ���� ����Ʈ
    [SerializeField] private List<WeaponSlot> rightWeaponList; // ������ ���� ����Ʈ
    [SerializeField] private List<ConsumptionSlot> attackC_List; // ���ݿ� �Ҹ�ǰ ����Ʈ
    [SerializeField] private List<ConsumptionSlot> recoveryC_List; // ȸ���� �Ҹ�ǰ ����Ʈ
    public int leftArmNum = 0;
    public int rightArmNum = 0;
    public int attackC_Num = 0;
    public int recoveryC_Num = 0;

    // { Property
    public ItemData QuickSlotRightWeapon { get { return rightArm.Item; } }
    public ItemData QuickSlotLeftWeapon { get { return leftArm.Item; } }
    public ItemData QuickSlotAttackConsumption { get { return attackC.Item; } }
    public ItemData QuickSlotRecoveryConsumption { get { return recoveryC.Item; } }
    // } Property
    public GameObject GetCurrentRightWeaponObject { get { return rightWeaponList[rightArmNum].equipItem; } }
    public GameObject GetCurrentLeftWeaponObject { get { return leftWeaponList[leftArmNum].equipItem; } }
    public GameObject GetCurrentAttackConsumptionObject { get { return attackC_List[attackC_Num].equipItem; } }
    public GameObject GetCurrentRecoveryConsumptionObject { get { return recoveryC_List[recoveryC_Num].equipItem; } }

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

    //! ������ ��� Ŀ���
    private void InPutQuickSlot()
    {
        if (GameManager.Instance.CheckActiveTitleScene() == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                // ���콺 ���� ���� ��ũ������ �� ������ ���� ����
                if (scroll > 0f)
                {
                    if (rightWeaponList[rightArmNum].Item != null)
                    {
                        // ���� ������ ������ ������Ʈ ��Ȱ��ȭ
                        rightWeaponList[rightArmNum].equipItem.SetActive(false);
                    }
                    // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                    if (rightArmNum == rightWeaponList.Count - 1)
                    {
                        rightArmNum = -1;
                    }
                    rightArmNum++;
                    rightArm.Item = rightWeaponList[rightArmNum].Item;
                    Inventory.Instance._onEquipSlotUpdated();
                    // �����Կ� ���Ⱑ ���� ��
                    if (rightWeaponList[rightArmNum].Item != null)
                    {
                        // ���� ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                        rightWeaponList[rightArmNum].equipItem.SetActive(true);
                        //Debug.Log($"����Ʈ+�پ� : {rightArmNum}");
                    }
                }
                // ���콺 ���� �Ʒ��� ��ũ������ �� �޼� ���� ����
                else if (scroll < 0f)
                {
                    if (leftWeaponList[leftArmNum].Item != null)
                    {
                        // ���� ������ ������ ������Ʈ ��Ȱ��ȭ
                        leftWeaponList[leftArmNum].equipItem.SetActive(false);
                    }
                    // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                    if (leftArmNum == leftWeaponList.Count - 1)
                    {
                        leftArmNum = -1;
                    }
                    leftArmNum++;
                    leftArm.Item = leftWeaponList[leftArmNum].Item;
                    Inventory.Instance._onEquipSlotUpdated();
                    // �����Կ� ���Ⱑ ���� ��
                    if (leftWeaponList[leftArmNum].Item != null)
                    {
                        // ���� ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                        leftWeaponList[leftArmNum].equipItem.SetActive(true);
                        //Debug.Log($"����Ʈ+�ٴٿ� : {leftArmNum}");
                    }
                }
            }
            else
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                // ���콺 ���� ���� ��ũ������ �� ���ݿ� �Ҹ�ǰ ����
                if (scroll > 0f)
                {
                    if (attackC_List[attackC_Num].Item != null)
                    {
                        // ���� ������ ������ ������Ʈ ��Ȱ��ȭ
                        attackC_List[attackC_Num].equipItem.SetActive(false);
                    }
                    // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                    if (attackC_Num == attackC_List.Count - 1)
                    {
                        attackC_Num = -1;
                    }
                    attackC_Num++;
                    attackC.Item = attackC_List[attackC_Num].Item;
                    Inventory.Instance._onEquipSlotUpdated();
                    // �����Կ� ���ݿ� �Ҹ�ǰ�� ���� ��
                    if (attackC_List[attackC_Num].Item != null)
                    {
                        // ���ݿ� �Ҹ�ǰ ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                    }
                }
                // ���콺 ���� �Ʒ��� ��ũ������ �� ȸ���� �Ҹ�ǰ ����
                else if (scroll < 0f)
                {
                    if (recoveryC_List[recoveryC_Num].Item != null)
                    {
                        // ���� ������ ������ ������Ʈ ��Ȱ��ȭ
                        recoveryC_List[recoveryC_Num].equipItem.SetActive(false);
                    }
                    // ������ ũ�⸦ ����� �ε��� �ʱ�ȭ
                    if (recoveryC_Num == recoveryC_List.Count - 1)
                    {
                        recoveryC_Num = -1;
                    }
                    recoveryC_Num++;
                    recoveryC.Item = recoveryC_List[recoveryC_Num].Item;
                    Inventory.Instance._onEquipSlotUpdated();
                    // �����Կ� ȸ���� �Ҹ�ǰ�� ���� ��
                    if (recoveryC_List[recoveryC_Num].Item != null)
                    {
                        // ȸ���� �Ҹ�ǰ ������Ʈ�� �÷��̾��� ���������� ��ġ �����ϰ� Ȱ��ȭ
                    }
                }
            }
        }
    } // InPutQuickSlot

    //! ������ �ε�� ������ ������ �����ϴ� �Լ�
    public void LoadQuickSlotData()
    {
        // ������ ���� ������ ����
        if (rightWeaponList[rightArmNum].Item != null && rightWeaponList[rightArmNum].equipItem != null)
        {
            rightArm.Item = rightWeaponList[rightArmNum].Item;
            rightWeaponList[rightArmNum].equipItem.SetActive(true);
        }
        else
        {
            rightArm.Item = null;
        }

        // �޼� ���� ������ ����
        if (leftWeaponList[leftArmNum].Item != null && leftWeaponList[leftArmNum].equipItem != null)
        {
            leftArm.Item = leftWeaponList[leftArmNum].Item;
            leftWeaponList[leftArmNum].equipItem.SetActive(true);
        }
        else
        {
            leftArm.Item = null;
        }

        // ���ݿ� �Ҹ�ǰ ������ ����
        if (attackC_List[attackC_Num].Item != null && attackC_List[attackC_Num].equipItem != null)
        {
            attackC.Item = attackC_List[attackC_Num].Item;
        }
        else
        {
            attackC.Item = null;
        }

        // ȸ���� �Ҹ�ǰ ������ ����
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
