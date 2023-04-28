using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;

public class WeaponSlot : MonoBehaviour, IPublicSlot, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private GameObject pointerEffect; // Ŀ���� ���Կ� ���� �� ���� ����Ʈ
    [SerializeField] private Sprite typeSprite; // ����κ� ��ܿ� ǥ�õ� ���� ��������Ʈ
    private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public GameObject equipItem; // ���Կ� ������ ���� ������ ������Ʈ
    public GameObject SlotObj { get { return gameObject; } }

    public IPublicSlot equipSlot; // ������ ��������
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
            }
            else
            {
                // �������� ������ ���İ� 0���� ����
                icon.color = new Color(1, 1, 1, 0);
            }
        }
    } // Item

    void Awake()
    {
        button = GetComponent<Button>();
        descriptionPanel = Inventory.Instance.descriptionPanel;
        button.onClick.AddListener(() =>
        {
            Debug.Log("���� ���� ������");
            Inventory.Instance.selectSlot = this;
            Inventory.Instance.InitSameTypeEquipSlot(slotType);
            Inventory.Instance.equipInvenImage.sprite = typeSprite;
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
            Debug.Log($"������ �Ϸ� ������ ������Ʈ : {equipItem.name}");
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
} // WeaponSlot
