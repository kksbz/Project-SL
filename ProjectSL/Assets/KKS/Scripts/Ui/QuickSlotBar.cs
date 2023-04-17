using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotBar : MonoBehaviour
{
    [SerializeField] private QuickSlot LeftArm; // 왼손 이미지
    [SerializeField] private QuickSlot RightArm; // 오른손 이미지
    [SerializeField] private QuickSlot consumption; // 소모품 이미지
    [SerializeField] private QuickSlot spell; // 스펠 이미지
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
                // 마우스 휠을 위로 스크롤했을 때 처리할 코드
                if (rightArmNum == 2)
                {
                    rightArmNum = -1;
                }
                rightArmNum++;
                RightArm.Item = Inventory.Instance.weaponSlotList[rightArmNum].Item;
                Debug.Log($"쉬프트+휠업 : {rightArmNum}");

            }
            else if (scroll < 0f)
            {
                // 마우스 휠을 아래로 스크롤했을 때 처리할 코드
                if (leftArmNum == 5)
                {
                    leftArmNum = 2;
                }
                leftArmNum++;
                LeftArm.Item = Inventory.Instance.weaponSlotList[leftArmNum].Item;
                Debug.Log($"쉬프트+휠다운 : {leftArmNum}");

            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // 마우스 휠을 위로 스크롤했을 때 처리할 코드
            }
            else if (scroll < 0f)
            {
                // 마우스 휠을 아래로 스크롤했을 때 처리할 코드
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
