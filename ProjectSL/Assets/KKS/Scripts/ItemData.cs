using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemID; // ������ȣ
    public string itemName; // �̸�
    public string itemType; // Ÿ��
    public int itemDamage; // ������
    public int buyPrice; // ���Ű���
    public int sellPrice; // �ǸŰ���
    public string description; // ����

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
