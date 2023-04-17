using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private QuickSlot LeftArm; // �޼� �̹���
    [SerializeField] private QuickSlot RightArm; // ������ �̹���
    [SerializeField] private QuickSlot consumption; // �Ҹ�ǰ �̹���
    [SerializeField] private QuickSlot spell; // ���� �̹���
    private int leftArmNum = 2;
    private int rightArmNum = -1;
    private int consumptionNum = -1;
    private int spellNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        LeftArm.Item = Inventory.Instance.weaponSlotList[0].Item;
        RightArm.Item = Inventory.Instance.weaponSlotList[3].Item;
        consumption.Item = Inventory.Instance.consumptionSlotList[0].Item;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // ���콺 ���� ���� ��ũ������ �� ó���� �ڵ�
                if (rightArmNum == 2)
                {
                    rightArmNum = -1;
                }
                rightArmNum++;
                RightArm.Item = Inventory.Instance.weaponSlotList[rightArmNum].Item;
                Debug.Log($"����Ʈ+�پ� : {rightArmNum}");

            }
            else if (scroll < 0f)
            {
                // ���콺 ���� �Ʒ��� ��ũ������ �� ó���� �ڵ�
                if (leftArmNum == 5)
                {
                    leftArmNum = 2;
                }
                leftArmNum++;
                LeftArm.Item = Inventory.Instance.weaponSlotList[leftArmNum].Item;
                Debug.Log($"����Ʈ+�ٴٿ� : {leftArmNum}");

            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // ���콺 ���� ���� ��ũ������ �� ó���� �ڵ�
            }
            else if (scroll < 0f)
            {
                // ���콺 ���� �Ʒ��� ��ũ������ �� ó���� �ڵ�
                if (consumptionNum == Inventory.Instance.consumptionSlotList.Count - 1)
                {
                    consumptionNum = -1;
                }
                consumptionNum++;
                consumption.Item = Inventory.Instance.consumptionSlotList[consumptionNum].Item;
            }
        }
    }
}
