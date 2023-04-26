using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public enum ItemType
    {
        NONE,
        RECOVERY_CONSUMPTION,
        ATTACK_CONSUMPTION,
        WEAPON,
        SHIELD,
        HELMET,
        CHEST,
        GLOVES,
        PANTS,
        RING
    } // ItemType

    public int itemID; // 고유번호
    public string itemName; // 이름
    public ItemType itemType; // 타입
    public int damage; // 공격력
    public int vigor; // hp
    public int attunement; // mp
    public int endurance; // st
    public int vitality; // 활력
    public int strength; // 힘
    public int dexterity; // 민첩
    public int buyPrice; // 구매가격
    public int sellPrice; // 판매가격
    public int maxQuantity; // 최대 수량
    public string description; // 설명
    public string itemIcon; // 이미지주소
    [SerializeField] private int quantity = 1; // 보유 수량
    public int Quantity { get { return quantity; } set { quantity = value; } }
    [SerializeField] private bool isEquip = false; // 장착 여부
    public bool IsEquip { get { return isEquip; } set { isEquip = value; } }
    public ItemData(string[] data)
    {
        itemID = int.Parse(data[0]);
        itemName = data[1];
        itemType = (ItemType)Enum.Parse(typeof(ItemType), data[2]);
        damage = int.Parse(data[3]);
        vigor = int.Parse(data[4]);
        attunement = int.Parse(data[5]);
        endurance = int.Parse(data[6]);
        vitality = int.Parse(data[7]);
        strength = int.Parse(data[8]);
        dexterity = int.Parse(data[9]);
        buyPrice = int.Parse(data[10]);
        sellPrice = int.Parse(data[11]);
        maxQuantity = int.Parse(data[12]);
        description = data[13].Replace("\"", "");
        itemIcon = data[14];
    } // ItemData
} // ItemData
