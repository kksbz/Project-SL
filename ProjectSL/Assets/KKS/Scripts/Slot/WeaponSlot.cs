using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;

public class WeaponSlot : MonoBehaviour, IPublicSlot, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    private ItemDescriptionPanel descriptionPanel; // 아이템 설명 패널
    [SerializeField] private ItemType slotType; // 슬롯에 담길 아이템타입 제한 변수
    public ItemType SlotType { get { return slotType; } set { slotType = value; } }
    [SerializeField] private ItemData item; // 슬롯에 담길 아이템 변수
    public ItemData Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                // 아이템이 있으면 이미지 출력
                icon.sprite = Resources.Load<Sprite>(item.itemIcon);
                icon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                // 아이템이 없으면 알파값 0으로 숨김
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
            Debug.Log("무기 슬롯 선택함");
            Inventory.Instance.selectSlot = this;
            Inventory.Instance.InitSameTypeEquipSlot(slotType);
            Inventory.Instance.equipInvenText.text = "무기";
            Inventory.Instance.equipInvenPanel.SetActive(true);
            Inventory.Instance.equipSlotPanel.SetActive(false);
        });
    } // Start

    public void AddItem(ItemData _item)
    {
        Item = _item;
    } // AddItem

    public void RemoveItem()
    {
        Item = null;
    } // RemoveItem

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Debug.Log("템있는 슬롯에 커서 들옴");
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // WeaponSlot
