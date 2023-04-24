using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class ItemTypeController : MonoBehaviour
{
    [SerializeField] private TMP_Text typeText; // 아이템타입 표시 텍스트
    [SerializeField] private Button leftBt; // 왼쪽 버튼
    [SerializeField] private Button RightBt; // 오른쪽 버튼
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // 아이템타입 리스트
    private List<string> typeNameToKr = new List<string>(); // 아이템타입 한글로 변환한 리스트
    private Dictionary<ItemType, string> typeDic = new Dictionary<ItemType, string>(); // 딕셔너리로 저장
    private int num = 0;
    public ItemType selectType; // 선택한 아이템타입
    // Start is called before the first frame update
    void Awake()
    {
        TypeNameToKr();
        RightBt.onClick.AddListener(() =>
        {
            if (num == typeList.Count - 1)
            {
                num = -1;
            }
            num++;
            Debug.Log($"오른쪽 버튼 클릭 : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
            typeText.text = typeDic[typeList[num]];
            Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
            selectType = typeList[num];
        });

        leftBt.onClick.AddListener(() =>
        {
            if (num == 0)
            {
                num = typeList.Count;
            }
            num--;
            Debug.Log($"왼쪽 버튼 클릭 : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
            typeText.text = typeDic[typeList[num]];
            Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
            selectType = typeList[num];
        });
    } // Start

    private void OnEnable()
    {
        Debug.Log("활성화됨");
        num = 0;
        typeText.text = typeDic[typeList[num]];
        Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
    } // OnEnable

    private void TypeNameToKr()
    {
        string typeName = default;
        for (int i = 0; i < typeList.Count; i++)
        {
            switch (typeList[i])
            {
                case ItemType.NONE:
                    typeName = "전체";
                    break;
                case ItemType.CONSUMPTION:
                    typeName = "소모품";
                    break;
                case ItemType.WEAPON:
                    typeName = "무기";
                    break;
                case ItemType.HELMET:
                    typeName = "투구";
                    break;
                case ItemType.CHEST:
                    typeName = "상의";
                    break;
                case ItemType.GLOVES:
                    typeName = "장갑";
                    break;
                case ItemType.PANTS:
                    typeName = "하의";
                    break;
                case ItemType.RING:
                    typeName = "반지";
                    break;
            }
            typeNameToKr.Add(typeName);
            typeDic.Add(typeList[i], typeNameToKr[i]);
        }
    } // TypeNameToKr
} // ItemTypeController
