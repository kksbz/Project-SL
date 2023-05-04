using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ShopPanel shopPanel; // 상점 패널
    [SerializeField] private Button button; // 슬롯 버튼
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    [SerializeField] private GameObject equipIcon; // 슬롯에 표시될 장착여부icon
    [SerializeField] private TMP_Text quantityText; // 슬롯에 표시될 보유수량 텍스트
    [SerializeField] private TMP_Text price; // 아이템 가격 텍스트
    [SerializeField] private ItemDescriptionPanel descriptionPanel; // 아이템 설명 패널
    [SerializeField] private ItemData item; // 슬롯에 담길 아이템 변수
    public ItemData Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                // 아이템이 있으면 이미지 및 가격 출력
                icon.sprite = Resources.Load<Sprite>(item.itemIcon);
                icon.color = new Color(1, 1, 1, 1);
                // 상점패널이 판매창 이라면
                if (shopPanel.isBuyPanel == false)
                {
                    price.text = item.sellPrice.ToString();
                    // 아이템 장착여부 표시
                    if (item.IsEquip == true)
                    {
                        equipIcon.SetActive(true);
                    }
                    else
                    {
                        equipIcon.SetActive(false);
                    }

                    // 아이템 보유 수량 표시
                    if (item.maxQuantity != 1)
                    {
                        quantityText.text = item.Quantity.ToString();
                        quantityText.gameObject.SetActive(true);
                    }
                    else
                    {
                        quantityText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    price.text = item.buyPrice.ToString();
                }
            }
            else
            {
                // 아이템이 없으면 알파값 0으로 숨김
                price.text = "";
                icon.color = new Color(1, 1, 1, 0);
                equipIcon.SetActive(false);
                quantityText.gameObject.SetActive(false);
            }
        }
    } // Item
    void Start()
    {
        // 슬롯 버튼
        button.onClick.AddListener(() =>
        {
            shopPanel.RenewalBuySellMessege(item);
        });
    } // Start

    //! 마우스 커서 들어올 때 실행
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
} // ShopSlot
