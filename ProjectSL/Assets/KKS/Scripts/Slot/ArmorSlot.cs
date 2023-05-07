using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;

public class ArmorSlot : MonoBehaviour, IPublicSlot, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    [SerializeField] private GameObject pointerEffect; // 커서가 슬롯에 들어올 시 나올 이펙트
    [SerializeField] List<Sprite> itemSprites = new List<Sprite>(); // 장비인벤 상단에 표시될 방어구 스프라이트 리스트
    private Sprite invenSprite; // 장비인벤 상단에 표시될 선택한 방어구 스프라이트
    private ItemDescriptionPanel descriptionPanel; // 아이템 설명 패널
    public GameObject SlotObj { get { return gameObject; } }

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
            Debug.Log("방어구 슬롯 선택함");
            Inventory.Instance.selectSlot = this;
            Inventory.Instance.InitSameTypeEquipSlot(slotType);
            ShowInvenText();
            Inventory.Instance.equipInvenImage.sprite = invenSprite;
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
    } // AddItem

    public void RemoveItem()
    {
        Item = null;
    } // RemoveItem

    public bool SlotItemIsNull()
    {
        if (Item == null)
        {
            return true;
        }
        return false;
    } // SlotItemIsNull

    private void ShowInvenText()
    {
        switch (slotType)
        {
            case ItemType.HELMET:
                invenSprite = itemSprites[0];
                break;
            case ItemType.CHEST:
                invenSprite = itemSprites[1];
                break;
            case ItemType.GLOVES:
                invenSprite = itemSprites[2];
                break;
            case ItemType.PANTS:
                invenSprite = itemSprites[3];
                break;
            case ItemType.RING:
                invenSprite = itemSprites[4];
                break;
        }
    } // ShowInvenText

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEffect.SetActive(true);
        if (item != null)
        {
            Debug.Log("템있는 슬롯에 커서 들옴");
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEffect.SetActive(false);
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // ArmorSlot
