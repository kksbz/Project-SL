using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject selectPanel; // ����â
    [SerializeField] private TMP_Text topLineText; // ��� �ؽ�Ʈ
    [SerializeField] private Image shopItemTypeIcon; // ������ ǥ�õ� ������Ÿ�� ������
    [SerializeField] private Button leftBt; // ���� ��ư
    [SerializeField] private Button rightBt; // ������ ��ư
    [SerializeField] private GameObject buySellMessege; // ����, �Ǹ� ����â
    [SerializeField] private GameObject warningPanel; // ���â
    [SerializeField] private TMP_Text warningText; // ���â
    [SerializeField] private TMP_Text buySellText; // ����, �Ǹ� �ȳ� �ؽ�Ʈ
    [SerializeField] private TMP_Text selectQuantityPanelText; // ���� �ȳ� �ؽ�Ʈ
    [SerializeField] private TMP_Text itemNameText; // ����, �Ǹ� ������ �̸� �ؽ�Ʈ
    [SerializeField] private TMP_Text priceText; // ����, �Ǹ� ������ ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text selectQuantityText; // ������ ���� �ؽ�Ʈ
    [SerializeField] private Button upBt; // ���� �� ��ư
    [SerializeField] private Button downBt; // ���� �ٿ� ��ư
    [SerializeField] private Button selectBt; // ����, �Ǹ� ���� ��ư
    [SerializeField] private Button cancleBt; // ����, �Ǹ� ��� ��ư
    [SerializeField] private TMP_Text soulText; // ���� �ҿ� �ؽ�Ʈ
    public List<ItemData> shopItems = new List<ItemData>(); // ���� �Ǹ� �����۵����� ����Ʈ
    [SerializeField] private List<ShopSlot> shopSlots = new List<ShopSlot>(); // ���� ���� ����Ʈ
    [SerializeField] private List<Sprite> itemSprites; // ������Ÿ�Ժ� ��������Ʈ ����Ʈ
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // ������Ÿ�� ����Ʈ
    private Dictionary<ItemType, Sprite> typeDic = new Dictionary<ItemType, Sprite>(); // ��ųʸ��� ����
    private int num = 0;
    private int buyItemQuantity; // ���� ����
    public bool isBuyPanel = true; // ����, �Ǹ� üũ ����
    public ItemData selectItem; // ������ ������
    private void Start()
    {
        // ���� ��ư
        leftBt.onClick.AddListener(() =>
        {
            if (num == 0)
            {
                num = typeList.Count;
            }
            num--;
            shopItemTypeIcon.sprite = typeDic[typeList[num]];

            if (isBuyPanel == true)
            {
                InitSameTypeShopSlot(typeList[num], shopItems);
            }
            else
            {
                InitSameTypeShopSlot(typeList[num], Inventory.Instance.inventory);
            }
        });
        // ������ ��ư
        rightBt.onClick.AddListener(() =>
        {
            if (num == typeList.Count - 1)
            {
                num = -1;
            }
            num++;
            shopItemTypeIcon.sprite = typeDic[typeList[num]];

            if (isBuyPanel == true)
            {
                InitSameTypeShopSlot(typeList[num], shopItems);
            }
            else
            {
                InitSameTypeShopSlot(typeList[num], Inventory.Instance.inventory);
            }
        });
        // ���� �� ��ư
        upBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                if (Inventory.Instance.Soul < ((buyItemQuantity + 1) * selectItem.buyPrice))
                {
                    warningText.text = "���� �ҿ��� �����մϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity += 1;
                priceText.text = (selectItem.buyPrice * buyItemQuantity).ToString();
            }
            else
            {
                if (selectItem.Quantity == buyItemQuantity)
                {
                    warningText.text = "�̹� �ִ� ���������Դϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity += 1;
                priceText.text = (selectItem.sellPrice * buyItemQuantity).ToString();
            }
            selectQuantityText.text = buyItemQuantity.ToString();
        });
        // ���� �ٿ� ��ư
        downBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                if (buyItemQuantity == 1)
                {
                    warningText.text = "�̹� �ּ� �����Դϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity -= 1;
                priceText.text = (selectItem.buyPrice * buyItemQuantity).ToString();
            }
            else
            {
                if (buyItemQuantity == 1)
                {
                    warningText.text = "�̹� �ּ� �����Դϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity -= 1;
                priceText.text = (selectItem.sellPrice * buyItemQuantity).ToString();
            }
            selectQuantityText.text = buyItemQuantity.ToString();
        });

        // ����, �Ǹ� ���� ��ư
        selectBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                // ������ ����
                if (Inventory.Instance.Soul < selectItem.buyPrice * buyItemQuantity)
                {
                    // ���� �ҿ��� ������ ���ݺ��� ������ ���â ���
                    warningText.text = "���� �ҿ��� �����մϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                UiManager.Instance.soulBag.GetSoul(-(selectItem.buyPrice * buyItemQuantity));
                soulText.text = Inventory.Instance.Soul.ToString();
                for (int i = 0; i < buyItemQuantity; i++)
                {
                    Inventory.Instance.AddItem(selectItem);
                }
            }
            else
            {
                // ������ �Ǹ�
                if (selectItem.IsEquip == true)
                {
                    // �������� �������� �Ǹ��� �� ���â ���
                    warningText.text = "�������� ������ �Դϴ�";
                    warningPanel.SetActive(true);
                    return;
                }
                UiManager.Instance.soulBag.GetSoul(selectItem.sellPrice * buyItemQuantity);
                soulText.text = Inventory.Instance.Soul.ToString();
                if (selectItem.Quantity > buyItemQuantity)
                {
                    for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
                    {
                        if (Inventory.Instance.inventory[i] == selectItem)
                        {
                            Inventory.Instance.inventory[i].Quantity -= buyItemQuantity;
                            break;
                        }
                    }
                }
                else
                {
                    Inventory.Instance.RemoveItem(selectItem);
                }
                InitSameTypeShopSlot(typeList[num], Inventory.Instance.inventory);
            }
            buySellMessege.SetActive(false);
        });
        // ����, �Ǹ� ��� ��ư
        cancleBt.onClick.AddListener(() =>
        {
            buySellMessege.SetActive(false);
        });
    } // Start

    private void OnEnable()
    {
        soulText.text = Inventory.Instance.Soul.ToString();
        num = 0;
        buyItemQuantity = 1;
        shopItemTypeIcon.sprite = typeDic[typeList[num]];
        if (isBuyPanel == true)
        {
            topLineText.text = "���� - ����";
            InitSameTypeShopSlot(ItemType.NONE, shopItems);
        }
        else
        {
            topLineText.text = "���� - �Ǹ�";
            InitSameTypeShopSlot(ItemType.NONE, Inventory.Instance.inventory);
        }
        buySellMessege.SetActive(false);
    } // OnEnable

    private void OnDisable()
    {
        selectPanel.SetActive(true);
    } // OnDisable

    private void Update()
    {
        if (warningPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                warningPanel.SetActive(false);
            }
        }
    } // Update

    //! ����, �Ǹ� �޽���â �����ϴ� �Լ�
    public void RenewalBuySellMessege(ItemData _item)
    {
        selectItem = _item;
        buyItemQuantity = 1;
        selectQuantityText.text = buyItemQuantity.ToString();
        if (isBuyPanel == true)
        {
            buySellText.text = "������ �������� ���� �Ͻðڽ��ϱ�?";
            priceText.text = _item.buyPrice.ToString();
        }
        else
        {
            buySellText.text = "������ �������� �Ǹ� �Ͻðڽ��ϱ�?";
            priceText.text = _item.sellPrice.ToString();
        }
        itemNameText.text = _item.itemName;
        buySellMessege.SetActive(true);
    } // RenewalBuySellMessege

    //! �������� �����ϴ� �Լ�
    private void InitSameTypeShopSlot(ItemType _itemType, List<ItemData> _itemList)
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].Item = null;
        }
        // �����г� = �����Ǹſ������ ����Ʈ, �Ǹ��г� = �κ��丮������ ����Ʈ �Ű������� ����
        List<ItemData> inven = _itemList;
        // NONE�̸� ���Ÿ���� �������� ������
        if (_itemType == ItemType.NONE)
        {
            // �κ��丮�� ������� ���� ��츸 itemID ������ ����
            inven = inven.Where(x => x != null && x.itemID != 1 && x.itemID != 2).OrderBy(x => x.itemID).ToList();
            for (int i = 0; i < inven.Count; i++)
            {
                shopSlots[i].Item = inven[i];
                shopSlots[i].gameObject.SetActive(true);
            }
            for (int i = inven.Count; i < shopSlots.Count; i++)
            {
                shopSlots[i].gameObject.SetActive(false);
            }
            return;
        }

        // _itemType�� ���� Ÿ���� �����۸� ������
        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inven)
        {
            // ���� Ÿ���� �����۸� ���� ĳ��
            if (_item != null && _item.itemType == _itemType && _item.itemID != 1 && _item.itemID != 2)
            {
                sameTypes.Add(_item);
            }
        }
        //Debug.Log($"{sameTypes.Count}");
        // itemID �������� �������� ����
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < sameTypes.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // ĳ���ص� ���� Ÿ���� �������� ���Կ� ǥ��
                shopSlots[i].Item = sameTypes[i];
                shopSlots[i].gameObject.SetActive(true);
            }
        }
        for (int i = sameTypes.Count; i < shopSlots.Count; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    } // InitSameTypeShopSlot

    //! ������Ÿ�Ժ� ��������Ʈ ��ųʸ��� �����ϴ� �Լ�
    public void GetTypeSprites()
    {
        List<Sprite> typeSprites = new List<Sprite>();
        Sprite itemTypeSprite = default;
        for (int i = 0; i < typeList.Count; i++)
        {
            switch (typeList[i])
            {
                case ItemType.NONE:
                    itemTypeSprite = itemSprites[0];
                    break;
                case ItemType.RECOVERY_CONSUMPTION:
                    itemTypeSprite = itemSprites[1];
                    break;
                case ItemType.ATTACK_CONSUMPTION:
                    itemTypeSprite = itemSprites[2];
                    break;
                case ItemType.WEAPON:
                    itemTypeSprite = itemSprites[3];
                    break;
                case ItemType.SHIELD:
                    itemTypeSprite = itemSprites[4];
                    break;
                case ItemType.HELMET:
                    itemTypeSprite = itemSprites[5];
                    break;
                case ItemType.CHEST:
                    itemTypeSprite = itemSprites[6];
                    break;
                case ItemType.GLOVES:
                    itemTypeSprite = itemSprites[7];
                    break;
                case ItemType.PANTS:
                    itemTypeSprite = itemSprites[8];
                    break;
                case ItemType.RING:
                    itemTypeSprite = itemSprites[9];
                    break;
            }
            typeSprites.Add(itemTypeSprite);
            typeDic.Add(typeList[i], typeSprites[i]);
        }
    } // GetTypeSprites
} // ShopPanel



