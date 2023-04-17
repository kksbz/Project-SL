using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class ItemData
{
    public enum ItemType
    {
        NONE,
        CONSUMPTION,
        WEAPON,
        HELMET,
        CHEST,
        GLOVES,
        PANTS,
        RING
    } // ItemType

    public int itemID; // ������ȣ
    public string itemName; // �̸�
    public ItemType itemType; // Ÿ��
    public int itemValue; // ���� (������ or ȸ����)
    public int buyPrice; // ���Ű���
    public int sellPrice; // �ǸŰ���
    public int maxQuantity; // �ִ� ����
    public string description; // ����
    public string itemIcon; // �̹����ּ�
    [SerializeField] private int quantity = 1; // ���� ����
    public int Quantity { get { return quantity; } set { quantity = value; } }
    [SerializeField] private bool isEquip = false; // ���� ����
    public bool IsEquip { get { return isEquip; } set { isEquip = value; } }
    public ItemData(string[] data)
    {
        itemID = int.Parse(data[0]);
        itemName = data[1];
        itemType = (ItemType)Enum.Parse(typeof(ItemType), data[2]);
        itemValue = int.Parse(data[3]);
        buyPrice = int.Parse(data[4]);
        sellPrice = int.Parse(data[5]);
        maxQuantity = int.Parse(data[6]);
        description = data[7].Replace("\"", "");
        itemIcon = data[8];
    } // ItemData
} // ItemData
