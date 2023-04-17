using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private GameObject equipIcon; // �������� ǥ�� icon
    [SerializeField] private TMP_Text quantity; // ����ǥ�� Text
    private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    private IPublicSlot equipSlot; // ������ ��� ����
    [SerializeField] private ItemData item; // ���Կ� ��� ������ ����
    public ItemData Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                // �������� ������ �̹��� ���
                icon.sprite = Resources.Load<Sprite>(item.itemIcon);
                icon.color = new Color(1, 1, 1, 1);
                // ���� ���� �ؽ�Ʈ ���
                if (item.maxQuantity != 1)
                {
                    quantity.text = item.Quantity.ToString();
                    quantity.gameObject.SetActive(true);
                }
                else
                {
                    quantity.gameObject.SetActive(false);
                }
                // ���� ���� �̹��� ���
                if (item.IsEquip == false)
                {
                    equipIcon.SetActive(false);
                }
                else
                {
                    equipIcon.SetActive(true);
                }
            }
            else
            {
                // �������� ������ ���İ� 0���� ����
                icon.color = new Color(1, 1, 1, 0);
                equipIcon.SetActive(false);
                quantity.gameObject.SetActive(false);
            }
        }
    } // Item

    void Awake()
    {
        button = GetComponent<Button>();
        descriptionPanel = Inventory.Instance.descriptionPanel;
        button.onClick.AddListener(() =>
        {
            if (item == null)
            {
                return;
            }
            if (item.IsEquip == false)
            {
                // ������ ����
                SelectSlot();
                Inventory.Instance.selectSlot.AddItem(item);
                Inventory.Instance.equipSlotPanel.SetActive(true);
                Inventory.Instance.equipInvenPanel.SetActive(false);
            }
            else
            {
                // ������ ���� ����
                equipSlot.RemoveItem();
            }
            item.IsEquip = !item.IsEquip;
            Inventory.Instance.InitSameTypeEquipSlot(item.itemType);
        });
    } // Start

    //! ���õ� ���� �������� �Լ�
    public void SelectSlot()
    {
        equipSlot = Inventory.Instance.selectSlot;
    } // SelectSlot

    //! ���콺 Ŀ�� ���� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // EquipSlot
