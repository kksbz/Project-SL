using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] private Button useBt; // ��� ��ư
    [SerializeField] private Image useBtImage;
    [SerializeField] private TMP_Text useBtText;
    [SerializeField] private Button throwBt; // ������ ��ư
    [SerializeField] private Image throwBtImage;
    [SerializeField] private TMP_Text throwBtText;
    [SerializeField] private Button destroyBt; // �ı� ��ư
    [SerializeField] private Image destroyBtImage;
    [SerializeField] private TMP_Text destroyBtText;
    [SerializeField] private Button cancelBt; // ��� ��ư
    [SerializeField] private ItemTypeController itemTypeController; // �����κ� ������Ÿ�� ��Ʈ�ѷ�
    private Slot slot; // ������ ����

    void Awake()
    {
        useBt.onClick.AddListener(() =>
        {
            if (slot.Item.itemType != ItemData.ItemType.CONSUMPTION)
            {
                return;
            }
            Inventory.Instance.equipSlotPanel.SetActive(true);
            Inventory.Instance.equipInvenPanel.SetActive(false);
            gameObject.SetActive(false);
        });

        throwBt.onClick.AddListener(() =>
        {
            if (slot.Item.IsEquip == true)
            {
                Debug.Log("�������� ������ �Դϴ�.");
                return;
            }
            Inventory.Instance.ThrowItem(slot.Item);
            Inventory.Instance.InitSameTypeTotalSlot(itemTypeController.selectType);
            gameObject.SetActive(false);
        });

        destroyBt.onClick.AddListener(() =>
        {
            if (slot.Item.IsEquip == true)
            {
                Debug.Log("�������� ������ �Դϴ�.");
                return;
            }
            Inventory.Instance.RemoveItem(slot.Item);
            Inventory.Instance.InitSameTypeTotalSlot(itemTypeController.selectType);
            gameObject.SetActive(false);
        });

        cancelBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    } // Start

    //! ������ �������� ���� ���ο� ���� ��ư ��� ���� �����ִ� �Լ�
    private void ShowButton()
    {
        if (slot.Item.itemType == ItemData.ItemType.CONSUMPTION)
        {
            useBtImage.color = new Color(1, 1, 1, 1);
            useBtText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            useBtImage.color = new Color(1, 1, 1, 0.5f);
            useBtText.color = new Color(1, 1, 1, 0.5f);
        }

        if (slot.Item.IsEquip == true)
        {
            throwBtImage.color = new Color(1, 1, 1, 0.5f);
            throwBtText.color = new Color(1, 1, 1, 0.5f);
            destroyBtImage.color = new Color(1, 1, 1, 0.5f);
            destroyBtText.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            throwBtImage.color = new Color(1, 1, 1, 1);
            throwBtText.color = new Color(1, 1, 1, 1);
            destroyBtImage.color = new Color(1, 1, 1, 1);
            destroyBtText.color = new Color(1, 1, 1, 1);
        }
    } // ShowButton

    //! ���õ� ���� �������� �Լ�
    public void SelectSlot(Slot _slot)
    {
        slot = _slot;
        ShowButton();
        Debug.Log($"���õ� ���� : {slot}");
    } // SelectSlot
} // SelectPanel
