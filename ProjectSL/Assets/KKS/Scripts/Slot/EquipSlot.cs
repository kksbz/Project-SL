using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    [SerializeField] private GameObject equipIcon; // 장착여부 표시 icon
    [SerializeField] private TMP_Text quantity; // 수량표시 Text
    [SerializeField] private GameObject pointerEffect; // 커서가 슬롯에 들어올 시 나올 이펙트
    private ItemDescriptionPanel descriptionPanel; // 아이템 설명 패널
    private EquipInvenController equipInvenController;
    public IPublicSlot equipSlot; // 선택한 장비 슬롯
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
        equipInvenController = Inventory.Instance.equipInvenController;
        button.onClick.AddListener(() =>
        {
            if (item == null)
            {
                return;
            }
            if (item.IsEquip == false)
            {
                // 아이템 장착
                SelectSlot();
                if (equipSlot.SlotItemIsNull() == false)
                {
                    equipInvenController.WarningPanel.SetActive(true);
                    return;
                }
                equipSlot.AddItem(item);
                Inventory.Instance.equipSlotPanel.SetActive(true);
                Inventory.Instance.equipInvenPanel.SetActive(false);
                UiManager.Instance.RenewalInvenStatusPanel();
            }
            else
            {
                // 아이템 장착 해제
                equipSlot.RemoveItem();
                UiManager.Instance.RenewalInvenStatusPanel();
            }
            item.IsEquip = !item.IsEquip;
            Inventory.Instance.InitSameTypeEquipSlot(item.itemType);
        });
    } // Start

    private void OnEnable()
    {
        Inventory.Instance.goBack.SetActive(true);
    } // OnEnable

    private void OnDisable()
    {
        Inventory.Instance.goBack.SetActive(false);
        pointerEffect.SetActive(false);
    } // OnDisable

    //! 선택된 슬롯 가져오는 함수
    public void SelectSlot()
    {
        equipSlot = Inventory.Instance.selectSlot;
    } // SelectSlot

    //! 마우스 커서 들어올 때 실행
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
