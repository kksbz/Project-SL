using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBar : MonoBehaviour
{
    public Button equip; // ���â
    public Button inven; // �����κ�
    public Button stat; // �÷��̾� ����
    public Button option; // �ɼ�
    public GameObject inventory;

    void Start()
    {
        equip.onClick.AddListener(() =>
        {
            Debug.Log("��� �κ� ������");
            inventory.SetActive(true);
            gameObject.SetActive(false);
        });

        inven.onClick.AddListener(() =>
        {
            Debug.Log("���� �κ� ������");
            inventory.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            Inventory.Instance.InitSameTypeTotalSlot(ItemData.ItemType.CONSUMPTION);
            Inventory.Instance.totalInvenPanel.SetActive(true);
            gameObject.SetActive(false);
        });

        stat.onClick.AddListener(() =>
        {
            Debug.Log("�������ͽ� ������");
            gameObject.SetActive(false);
        });

        option.onClick.AddListener(() =>
        {
            Debug.Log("�ɼ�â ������");
            gameObject.SetActive(false);
        });
    } // Start
} // QuickSlotBar
