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
    [SerializeField] private GameObject selectPanel; // 선택창
    [SerializeField] private TMP_Text topLineText; // 상단 텍스트
    [SerializeField] private Image shopItemTypeIcon; // 상점에 표시될 아이템타입 아이콘
    [SerializeField] private Button leftBt; // 왼쪽 버튼
    [SerializeField] private Button rightBt; // 오른쪽 버튼
    [SerializeField] private GameObject buySellMessege; // 구매, 판매 결정창
    [SerializeField] private GameObject warningPanel; // 경고창
    [SerializeField] private TMP_Text warningText; // 경고창
    [SerializeField] private TMP_Text buySellText; // 구매, 판매 안내 텍스트
    [SerializeField] private TMP_Text selectQuantityPanelText; // 수량 안내 텍스트
    [SerializeField] private TMP_Text itemNameText; // 구매, 판매 아이템 이름 텍스트
    [SerializeField] private TMP_Text priceText; // 구매, 판매 아이템 가격 텍스트
    [SerializeField] private TMP_Text selectQuantityText; // 선택한 수량 텍스트
    [SerializeField] private Button upBt; // 수량 업 버튼
    [SerializeField] private Button downBt; // 수량 다운 버튼
    [SerializeField] private Button selectBt; // 구매, 판매 결정 버튼
    [SerializeField] private Button cancleBt; // 구매, 판매 취소 버튼
    [SerializeField] private TMP_Text soulText; // 보유 소울 텍스트
    public List<ItemData> shopItems = new List<ItemData>(); // 상점 판매 아이템데이터 리스트
    [SerializeField] private List<ShopSlot> shopSlots = new List<ShopSlot>(); // 상점 슬롯 리스트
    [SerializeField] private List<Sprite> itemSprites; // 아이템타입별 스프라이트 리스트
    private List<ItemType> typeList = new List<ItemType>((ItemType[])Enum.GetValues(typeof(ItemType))); // 아이템타입 리스트
    private Dictionary<ItemType, Sprite> typeDic = new Dictionary<ItemType, Sprite>(); // 딕셔너리로 저장
    private int num = 0;
    private int buyItemQuantity; // 구매 수량
    public bool isBuyPanel = true; // 구매, 판매 체크 변수
    public ItemData selectItem; // 선택한 아이템
    private void Start()
    {
        // 왼쪽 버튼
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
        // 오른쪽 버튼
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
        // 수량 업 버튼
        upBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                if (Inventory.Instance.Soul < ((buyItemQuantity + 1) * selectItem.buyPrice))
                {
                    warningText.text = "보유 소울이 부족합니다";
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
                    warningText.text = "이미 최대 보유수량입니다";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity += 1;
                priceText.text = (selectItem.sellPrice * buyItemQuantity).ToString();
            }
            selectQuantityText.text = buyItemQuantity.ToString();
        });
        // 수량 다운 버튼
        downBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                if (buyItemQuantity == 1)
                {
                    warningText.text = "이미 최소 수량입니다";
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
                    warningText.text = "이미 최소 수량입니다";
                    warningPanel.SetActive(true);
                    return;
                }
                buyItemQuantity -= 1;
                priceText.text = (selectItem.sellPrice * buyItemQuantity).ToString();
            }
            selectQuantityText.text = buyItemQuantity.ToString();
        });

        // 구매, 판매 결정 버튼
        selectBt.onClick.AddListener(() =>
        {
            if (isBuyPanel == true)
            {
                // 아이템 구매
                if (Inventory.Instance.Soul < selectItem.buyPrice * buyItemQuantity)
                {
                    // 보유 소울이 아이템 가격보다 적으면 경고창 출력
                    warningText.text = "보유 소울이 부족합니다";
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
                // 아이템 판매
                if (selectItem.IsEquip == true)
                {
                    // 장착중인 아이템을 판매할 시 경고창 출력
                    warningText.text = "장착중인 아이템 입니다";
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
        // 구매, 판매 취소 버튼
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
            topLineText.text = "상점 - 구매";
            InitSameTypeShopSlot(ItemType.NONE, shopItems);
        }
        else
        {
            topLineText.text = "상점 - 판매";
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

    //! 구매, 판매 메시지창 갱신하는 함수
    public void RenewalBuySellMessege(ItemData _item)
    {
        selectItem = _item;
        buyItemQuantity = 1;
        selectQuantityText.text = buyItemQuantity.ToString();
        if (isBuyPanel == true)
        {
            buySellText.text = "선택한 아이템을 구매 하시겠습니까?";
            priceText.text = _item.buyPrice.ToString();
        }
        else
        {
            buySellText.text = "선택한 아이템을 판매 하시겠습니까?";
            priceText.text = _item.sellPrice.ToString();
        }
        itemNameText.text = _item.itemName;
        buySellMessege.SetActive(true);
    } // RenewalBuySellMessege

    //! 상점슬롯 갱신하는 함수
    private void InitSameTypeShopSlot(ItemType _itemType, List<ItemData> _itemList)
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].Item = null;
        }
        // 구매패널 = 상점판매용아이템 리스트, 판매패널 = 인벤토리아이템 리스트 매개변수로 받음
        List<ItemData> inven = _itemList;
        // NONE이면 모든타입의 아이템을 보여줌
        if (_itemType == ItemType.NONE)
        {
            // 인벤토리가 비어있지 않을 경우만 itemID 순으로 정렬
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

        // _itemType와 같은 타입의 아이템만 보여줌
        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inven)
        {
            // 같은 타입의 아이템만 따로 캐싱
            if (_item != null && _item.itemType == _itemType && _item.itemID != 1 && _item.itemID != 2)
            {
                sameTypes.Add(_item);
            }
        }
        //Debug.Log($"{sameTypes.Count}");
        // itemID 기준으로 오름차순 정렬
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < sameTypes.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // 캐싱해둔 같은 타입의 아이템을 슬롯에 표시
                shopSlots[i].Item = sameTypes[i];
                shopSlots[i].gameObject.SetActive(true);
            }
        }
        for (int i = sameTypes.Count; i < shopSlots.Count; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    } // InitSameTypeShopSlot

    //! 아이템타입별 스프라이트 딕셔너리로 저장하는 함수
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



