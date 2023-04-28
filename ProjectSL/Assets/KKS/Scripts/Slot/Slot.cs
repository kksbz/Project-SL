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
    [SerializeField] private Image icon; // ���Կ� ǥ�õ� icon
    [SerializeField] private GameObject equipIcon; // �������� ǥ�� icon
    [SerializeField] private TMP_Text quantity; // ����ǥ�� Text
    [SerializeField] private GameObject pointerEffect; // Ŀ���� ���Կ� ���� �� ���� ����Ʈ
    private SelectPanel selectPanel; // ����â �г�
    private ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public GameObject SlotObj { get { return gameObject; } }
    [SerializeField] private ItemType slotType; // ���Կ� ��� ������Ÿ�� ���� ����
    public ItemType SlotType { get { return slotType; } set { slotType = value; } }
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
                // ���� ���� �ؽ�Ʈ ���
                if (item.maxQuantity != 1)
                {
                    quantity.text = item.Quantity.ToString();
                    quantity.gameObject.SetActive(true);
                }
                else
                {
                    quantity.gameObject.SetActive(false);
                }
                // ���� ���� �̹��� ���
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
                // �������� ������ ���İ� 0���� ����
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
            Debug.Log("���� ������");
            // ����â�� ��ġ�� ������ ���ʰ� ��ġ�����ְ� �ű⿡ ������ x���̸�ŭ ���������� ������
            float xPos = (panelRect.sizeDelta.x - buttonRect.sizeDelta.x) * 0.5f + buttonRect.sizeDelta.x;
            // ����â�� ��ġ�� ������ ���������� ����
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEffect.SetActive(true);
        if (item != null)
        {
            Debug.Log("���ִ� ���Կ� Ŀ�� ���");
            descriptionPanel.ShowItemData(item);
        }
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerEffect.SetActive(false);
        descriptionPanel.HideItemData();
    } // OnPointerExit
} // Slot
