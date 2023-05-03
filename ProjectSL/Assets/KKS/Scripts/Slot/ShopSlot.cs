using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button; // ���� ��ư
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
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
                price.text = item.buyPrice.ToString();
                icon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                // �������� ������ ���İ� 0���� ����
                price.text = "";
                icon.color = new Color(1, 1, 1, 0);
            }
        }
    } // Item
    void Start()
    {
        // ���� ��ư
        button.onClick.AddListener(() =>
        {

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
