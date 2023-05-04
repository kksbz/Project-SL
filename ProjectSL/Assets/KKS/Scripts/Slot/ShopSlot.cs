using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ShopPanel shopPanel; // ���� �г�
    [SerializeField] private Button button; // ���� ��ư
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private GameObject equipIcon; // ���Կ� ǥ�õ� ��������icon
    [SerializeField] private TMP_Text quantityText; // ���Կ� ǥ�õ� �������� �ؽ�Ʈ
    [SerializeField] private TMP_Text price; // ������ ���� �ؽ�Ʈ
    [SerializeField] private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    [SerializeField] private ItemData item; // ���Կ� ��� ������ ����
    public ItemData Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item != null)
            {
                // �������� ������ �̹��� �� ���� ���
                icon.sprite = Resources.Load<Sprite>(item.itemIcon);
                icon.color = new Color(1, 1, 1, 1);
                // �����г��� �Ǹ�â �̶��
                if (shopPanel.isBuyPanel == false)
                {
                    price.text = item.sellPrice.ToString();
                    // ������ �������� ǥ��
                    if (item.IsEquip == true)
                    {
                        equipIcon.SetActive(true);
                    }
                    else
                    {
                        equipIcon.SetActive(false);
                    }

                    // ������ ���� ���� ǥ��
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
                // �������� ������ ���İ� 0���� ����
                price.text = "";
                icon.color = new Color(1, 1, 1, 0);
                equipIcon.SetActive(false);
                quantityText.gameObject.SetActive(false);
            }
        }
    } // Item
    void Start()
    {
        // ���� ��ư
        button.onClick.AddListener(() =>
        {
            shopPanel.RenewalBuySellMessege(item);
        });
    } // Start

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
} // ShopSlot
