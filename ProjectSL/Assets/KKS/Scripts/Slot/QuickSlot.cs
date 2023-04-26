
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private Image icon; // 슬롯에 표시될 icon
    [SerializeField] private TMP_Text quantity; // 수량표시 Text
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
                if (quantity != null)
                {
                    quantity.text = item.Quantity.ToString();
                    quantity.gameObject.SetActive(true);
                }
            }
            else
            {
                // 아이템이 없으면 알파값 0으로 숨김
                icon.color = new Color(1, 1, 1, 0);
                if (quantity != null)
                {
                    quantity.gameObject.SetActive(false);
                }
            }
        }
    } // Item

    public void AddItem(ItemData _item)
    {
        Item = _item;
    } // AddItem

    public void RemoveItem()
    {
        Item = null;
    } // RemoveItem
} // QuickSlot
