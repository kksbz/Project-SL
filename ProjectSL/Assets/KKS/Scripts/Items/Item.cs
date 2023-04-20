using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private int itemID; // ������ ������ ���ο� ���� ID
    public GameObject pickupArea;
    public ItemData itemData; // ������ ������

    void Awake()
    {
        foreach (string[] _itemData in DataManager.Instance.itemDatas)
        {
            // �����۵����� ���̺��� �ڱ� ID�� �����۵����͸� ������
            if (int.Parse(_itemData[0]) == itemID)
            {
                itemData = new ItemData(_itemData);
            }
        }
    } // Start

    // ������ ���� �� ������ �����͵� ����
    void OnDestroy()
    {
        itemData = null;
    } // OnDestroy
} // Item
