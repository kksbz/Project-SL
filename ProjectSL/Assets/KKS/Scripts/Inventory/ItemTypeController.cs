using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class ItemTypeController : MonoBehaviour
{
    [SerializeField] private TMP_Text typeText; // ������Ÿ�� ǥ�� �ؽ�Ʈ
    [SerializeField] private Button leftBt; // ���� ��ư
    [SerializeField] private Button RightBt; // ������ ��ư
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // ������Ÿ�� ����Ʈ
    private List<string> typeNameToKr = new List<string>(); // ������Ÿ�� �ѱ۷� ��ȯ�� ����Ʈ
    private Dictionary<ItemType, string> typeDic = new Dictionary<ItemType, string>(); // ��ųʸ��� ����
    private int num = 0;
    public ItemType selectType; // ������ ������Ÿ��
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
            Debug.Log($"������ ��ư Ŭ�� : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
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
            Debug.Log($"���� ��ư Ŭ�� : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
            typeText.text = typeDic[typeList[num]];
            Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
            selectType = typeList[num];
        });
    } // Start

    private void OnEnable()
    {
        Debug.Log("Ȱ��ȭ��");
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
                    typeName = "��ü";
                    break;
                case ItemType.RECOVERY_CONSUMPTION:
                    typeName = "ȸ���� �Ҹ�ǰ";
                    break;
                case ItemType.ATTACK_CONSUMPTION:
                    typeName = "���ݿ� �Ҹ�ǰ";
                    break;
                case ItemType.WEAPON:
                    typeName = "����";
                    break;
                case ItemType.SHIELD:
                    typeName = "����";
                    break;
                case ItemType.HELMET:
                    typeName = "����";
                    break;
                case ItemType.CHEST:
                    typeName = "����";
                    break;
                case ItemType.GLOVES:
                    typeName = "�尩";
                    break;
                case ItemType.PANTS:
                    typeName = "����";
                    break;
                case ItemType.RING:
                    typeName = "����";
                    break;
            }
            typeNameToKr.Add(typeName);
            typeDic.Add(typeList[i], typeNameToKr[i]);
        }
    } // TypeNameToKr
} // ItemTypeController
