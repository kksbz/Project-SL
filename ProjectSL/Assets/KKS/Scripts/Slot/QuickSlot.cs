
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private TMP_Text quantity; // ����ǥ�� Text
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
                if (quantity != null)
                {
                    quantity.text = item.Quantity.ToString();
                    quantity.gameObject.SetActive(true);
                }
            }
            else
            {
                // �������� ������ ���İ� 0���� ����
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
