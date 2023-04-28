using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;

public class ConsumptionSlot : MonoBehaviour, IPublicSlot, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private TMP_Text quantity; // ����ǥ�� Text
    [SerializeField] private GameObject pointerEffect; // Ŀ���� ���Կ� ���� �� ���� ����Ʈ
    [SerializeField] private Sprite attack_TypeSprite; // ����κ� ��ܿ� ǥ�õ� ���ݿ� �Ҹ�ǰ ��������Ʈ
    [SerializeField] private Sprite recovery_TypeSprite; // ����κ� ��ܿ� ǥ�õ� ȸ���� �Ҹ�ǰ ��������Ʈ
    private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public GameObject equipItem; // ���Կ� ������ �Ҹ�ǰ ������ ������Ʈ
    public GameObject SlotObj { get { return gameObject; } }

    [SerializeField] private ItemType slotType; // ���Կ� ��� ������Ÿ�� ���� ����
    public ItemType SlotType { get { return slotType; } set { slotType = value; } }
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
                quantity.text = item.Quantity.ToString();
                quantity.gameObject.SetActive(true);
            }
            else
            {
                // �������� ������ ���İ� 0���� ����
                icon.color = new Color(1, 1, 1, 0);
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
            Debug.Log("�Ҹ�ǰ ���� ������");
            Inventory.Instance.selectSlot = this;
            Inventory.Instance.InitSameTypeEquipSlot(slotType);
            if (slotType == ItemType.ATTACK_CONSUMPTION)
            {
                Inventory.Instance.equipInvenImage.sprite = attack_TypeSprite;
            }
            else
            {
                Inventory.Instance.equipInvenImage.sprite = recovery_TypeSprite;
            }
            Inventory.Instance.equipInvenPanel.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
            pointerEffect.SetActive(false);
        });
    } // Start

    private void OnDisable()
    {
        pointerEffect.SetActive(false);
    } // OnDisable


    public void AddItem(ItemData _item)
    {
        Item = _item;
        if (_item != null)
        {
            equipItem = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{_item.itemID}"));
            equipItem.GetComponent<Item>().pickupArea.SetActive(false);
            equipItem.SetActive(false);
            UiManager.Instance.quickSlotBar.LoadQuickSlotData();
        }
    } // AddItem

    public void RemoveItem()
    {
        Item = null;
        // ������ ������ �ı�
        Destroy(equipItem);
        equipItem = null;
        UiManager.Instance.quickSlotBar.LoadQuickSlotData();
    } // RemoveItem

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEffect.SetActive(true);
        if (item != null)
        {
            Debug.Log("���ִ� ���Կ� Ŀ�� ���");
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEffect.SetActive(false);
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // ConsumptionSlot
