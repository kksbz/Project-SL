using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;

public class Slot : MonoBehaviour, IPublicSlot, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    [SerializeField] private GameObject equipIcon; // 장착여부 표시 icon
    [SerializeField] private TMP_Text quantity; // 수량표시 Text
    [SerializeField] private GameObject pointerEffect; // 커서가 슬롯에 들어올 시 나올 이펙트
    private SelectPanel selectPanel; // 선택창 패널
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
                // 보유 수량 텍스트 출력
                if (item.maxQuantity != 1)
                {
                    quantity.text = item.Quantity.ToString();
                    quantity.gameObject.SetActive(true);
                }
                else
                {
                    quantity.gameObject.SetActive(false);
                }
                // 장착 여부 이미지 출력
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
                // 아이템이 없으면 알파값 0으로 숨김
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
        selectPanel = Inventory.Instance.selectPanel;
        RectTransform panelRect = selectPanel.GetComponent<RectTransform>();
        RectTransform buttonRect = gameObject.GetComponent<RectTransform>();
        button.onClick.AddListener(() =>
        {
            if (item == null)
            {
                return;
            }
            Debug.Log("슬롯 선택함");
            // 설명창의 위치를 슬롯의 왼쪽과 일치시켜주고 거기에 슬롯의 x길이만큼 오른쪽으로 더해줌
            float xPos = (panelRect.sizeDelta.x - buttonRect.sizeDelta.x) * 0.5f + buttonRect.sizeDelta.x;
            // 설명창의 위치를 슬롯의 오른쪽으로 설정
            selectPanel.transform.position = transform.position + new Vector3(xPos, 0, 0);
            selectPanel.gameObject.SetActive(true);
            selectPanel.SelectSlot(this);
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
} // Slot
