using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private int itemID; // ������ ������ ���ο� ���� ID
    public GameObject pickupArea; // ������ ȹ�氡�� ����������Ʈ
    public ItemData itemData; // ������ ������

    void Awake()
    {
        foreach (string[] _itemData in DataManager.Instance.itemDatas)
        {
            // �����۵����� ���̺��� �ڱ� ID�� �����۵����͸� ������
            int id = 0;
            int.TryParse(_itemData[0], out id);
            //Debug.Log($"{_itemData[0]} = {id}�� ������Ÿ�� : {id.GetType()}");
            if (id == itemID)
            {
                itemData = new ItemData(_itemData);
                break;
            }
        }
    } // Start

    // ������ ���� �� ������ �����͵� ����
    void OnDestroy()
    {
        itemData = null;
    } // OnDestroy
} // Item
