using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class ItemTypeController : MonoBehaviour
{
    [SerializeField] private Image typeImage; // 아이템타입 이미지
    [SerializeField] private List<Sprite> itemSprites; // 스프라이트 리스트
    [SerializeField] private Button leftBt; // 왼쪽 버튼
    [SerializeField] private Button RightBt; // 오른쪽 버튼
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // 아이템타입 리스트
    private List<Sprite> typeSprites = new List<Sprite>(); // 아이템타입별 스프라이트 리스트
    private Dictionary<ItemType, Sprite> typeDic = new Dictionary<ItemType, Sprite>(); // 딕셔너리로 저장
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
            Debug.Log($"오른쪽 버튼 클릭 : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
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
            Debug.Log($"왼쪽 버튼 클릭 : {num}, {typeDic[typeList[num]]}, {typeList[num]}");
            typeImage.sprite = typeDic[typeList[num]];
            Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
            selectType = typeList[num];
        });
    } // Start

    private void OnEnable()
    {
        Debug.Log("활성화됨");
        num = 0;
        typeImage.sprite = typeDic[typeList[num]];
        Inventory.Instance.InitSameTypeTotalSlot(typeList[num]);
        selectType = typeList[num];
    } // OnEnable

    //! 아이템타입별 스프라이트 딕셔너리로 저장하는 함수
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
