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
    public Sprite equipInven;
    public Sprite totalInven;
    void Start()
    {
        equip.onClick.AddListener(() =>
        {
            Debug.Log("��� �κ� ������");
            Inventory.Instance.topImage.sprite = equipInven;
            Inventory.Instance.topText.text = "���";
            inventory.SetActive(true);
            gameObject.SetActive(false);
            UiManager.Instance.RenewalInvenStatusPanel();
        });

        inven.onClick.AddListener(() =>
        {
            Debug.Log("���� �κ� ������");
            Inventory.Instance.topImage.sprite = totalInven;
            Inventory.Instance.topText.text = "�κ��丮";
            UiManager.Instance.RenewalInvenStatusPanel();
            inventory.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            Inventory.Instance.totalInvenPanel.SetActive(true);
            gameObject.SetActive(false);
        });

        stat.onClick.AddListener(() =>
        {
            Debug.Log("�������ͽ� ������");
            UiManager.Instance.RenewalStatusPanel();
            UiManager.Instance.statusPanel.gameObject.SetActive(true);
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
            UiManager.Instance.optionPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    } // Start
} // QuickSlotBar
