using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] private Button useBt; // 사용 버튼
    [SerializeField] private Image useBtImage;
    [SerializeField] private TMP_Text useBtText;
    [SerializeField] private Button throwBt; // 버리기 버튼
    [SerializeField] private Image throwBtImage;
    [SerializeField] private TMP_Text throwBtText;
    [SerializeField] private Button destroyBt; // 파괴 버튼
    [SerializeField] private Image destroyBtImage;
    [SerializeField] private TMP_Text destroyBtText;
    [SerializeField] private Button cancelBt; // 취소 버튼
    [SerializeField] private ItemTypeController itemTypeController; // 통합인벤 아이템타입 컨트롤러
    private Slot slot; // 선택한 슬롯

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
                Debug.Log("장착중인 아이템 입니다.");
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
                Debug.Log("장착중인 아이템 입니다.");
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

    //! 슬롯의 아이템의 장착 여부에 따라 버튼 사용 여부 보여주는 함수
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

    //! 선택된 슬롯 가져오는 함수
    public void SelectSlot(Slot _slot)
    {
        slot = _slot;
        ShowButton();
        Debug.Log($"선택된 슬롯 : {slot}");
    } // SelectSlot
} // SelectPanel
