using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBar : MonoBehaviour
{
    public Button equip; // ���â
    public Button inven; // �����κ�
    public Button stat; // �÷��̾� ����
    public Button warp; // ȭ��� �̵�â
    public Button option; // �ɼ�
    public GameObject inventory;

    void Start()
    {
        equip.onClick.AddListener(() =>
        {
            Debug.Log("��� �κ� ������");
            inventory.SetActive(true);
            gameObject.SetActive(false);
            UiManager.Instance.RenewalstatusPanel();
        });

        inven.onClick.AddListener(() =>
        {
            Debug.Log("���� �κ� ������");
            inventory.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            Inventory.Instance.totalInvenPanel.SetActive(true);
            gameObject.SetActive(false);
            UiManager.Instance.RenewalstatusPanel();
        });

        stat.onClick.AddListener(() =>
        {
            Debug.Log("�������ͽ� ������");
            gameObject.SetActive(false);
        });

        warp.onClick.AddListener(() =>
        {
            Debug.Log("ȭ��� �̵�â ������");
            gameObject.SetActive(false);
        });

        option.onClick.AddListener(() =>
        {
            Debug.Log("�ɼ�â ������");
            gameObject.SetActive(false);
        });
    } // Start
} // QuickSlotBar
