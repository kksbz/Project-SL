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
    [SerializeField] private GameObject pointerEffect; // Ŀ���� ���Կ� ���� �� ���� ����Ʈ
    private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public IPublicSlot equipSlot; // ������ ��� ����
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
                equipSlot.AddItem(item);
                Inventory.Instance.equipSlotPanel.SetActive(true);
                Inventory.Instance.equipInvenPanel.SetActive(false);
                UiManager.Instance.RenewalstatusPanel();
            }
            else
            {
                // ������ ���� ����
                equipSlot.RemoveItem();
                UiManager.Instance.RenewalstatusPanel();
            }
            item.IsEquip = !item.IsEquip;
            Inventory.Instance.InitSameTypeEquipSlot(item.itemType);
        });
    } // Start

    private void OnDisable()
    {
        pointerEffect.SetActive(false);
    } // OnDisable

    //! ���õ� ���� �������� �Լ�
    public void SelectSlot()
    {
        equipSlot = Inventory.Instance.selectSlot;
    } // SelectSlot

    //! ���콺 Ŀ�� ���� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEffect.SetActive(true);
        if (item != null)
        {
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEffect.SetActive(false);
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // EquipSlot
