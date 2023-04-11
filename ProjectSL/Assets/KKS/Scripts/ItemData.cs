using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemID; // 고유번호
    public string itemName; // 이름
    public string itemType; // 타입
    public int itemDamage; // 데미지
    public int buyPrice; // 구매가격
    public int sellPrice; // 판매가격
    public string description; // 설명

    public ItemData(string[] data)
    {
        itemID = int.Parse(data[0]);
        itemName = data[1];
        itemType = data[2];
        itemDamage = int.Parse(data[3]);
        buyPrice = int.Parse(data[4]);
        sellPrice = int.Parse(data[5]);
        description = data[6];
    } // ItemData
} // ItemData
