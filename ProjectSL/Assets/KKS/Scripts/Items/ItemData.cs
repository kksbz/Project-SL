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

    public int itemID; // ������ȣ
    public string itemName; // �̸�
    public ItemType itemType; // Ÿ��
    public int damage; // ���ݷ�
    public int vigor; // �����
    public int attunement; // ���߷�
    public int endurance; // ������
    public int vitality; // ü��
    public int strength; // �ٷ�
    public int dexterity; // �ⷮ
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
