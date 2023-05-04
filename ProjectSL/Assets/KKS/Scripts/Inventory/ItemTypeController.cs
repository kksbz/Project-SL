using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class ItemTypeController : MonoBehaviour
{
    [SerializeField] private Image typeImage; // ������Ÿ�� �̹���
    [SerializeField] private List<Sprite> itemSprites; // ��������Ʈ ����Ʈ
    [SerializeField] private Button leftBt; // ���� ��ư
    [SerializeField] private Button RightBt; // ������ ��ư
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // ������Ÿ�� ����Ʈ
    private List<Sprite> typeSprites = new List<Sprite>(); // ������Ÿ�Ժ� ��������Ʈ ����Ʈ
    private Dictionary<ItemType, Sprite> typeDic = new Dictionary<ItemType, Sprite>(); // ��ųʸ��� ����
    private int num = 0;
    public ItemType selectType;
    void Awake()
    {
        GetTypeSprites();
        RightBt.onClick.AddListener(() =>
        {
            if (num == typeList.Count - 1)
            {
                num = -1;
            }
            num++;
            Debug.Log($"������ ��ư Ŭ�� : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
            typeImage.sprite = typeDic[typeList[num]];
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
            typeImage.sprite = typeDic[typeList[num]];
            Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
            selectType = typeList[num];
        });
    } // Start

    private void OnEnable()
    {
        Debug.Log("Ȱ��ȭ��");
        num = 0;
        typeImage.sprite = typeDic[typeList[num]];
        Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
        selectType = typeList[num];
    } // OnEnable

    //! ������Ÿ�Ժ� ��������Ʈ ��ųʸ��� �����ϴ� �Լ�
    private void GetTypeSprites()
    {
        Sprite itemTypeSprite = default;
        for (int i = 0; i < typeList.Count; i++)
        {
            switch (typeList[i])
            {
                case ItemType.NONE:
                    itemTypeSprite = itemSprites[0];
                    break;
                case ItemType.RECOVERY_CONSUMPTION:
                    itemTypeSprite = itemSprites[1];
                    break;
                case ItemType.ATTACK_CONSUMPTION:
                    itemTypeSprite = itemSprites[2];
                    break;
                case ItemType.WEAPON:
                    itemTypeSprite = itemSprites[3];
                    break;
                case ItemType.SHIELD:
                    itemTypeSprite = itemSprites[4];
                    break;
                case ItemType.HELMET:
                    itemTypeSprite = itemSprites[5];
                    break;
                case ItemType.CHEST:
                    itemTypeSprite = itemSprites[6];
                    break;
                case ItemType.GLOVES:
                    itemTypeSprite = itemSprites[7];
                    break;
                case ItemType.PANTS:
                    itemTypeSprite = itemSprites[8];
                    break;
                case ItemType.RING:
                    itemTypeSprite = itemSprites[9];
                    break;
            }
            typeSprites.Add(itemTypeSprite);
            typeDic.Add(typeList[i], typeSprites[i]);
        }
    } // GetTypeSprites
} // ItemTypeController
