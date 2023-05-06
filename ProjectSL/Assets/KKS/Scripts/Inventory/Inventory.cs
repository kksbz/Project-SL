using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class Inventory : Singleton<Inventory>
{
    private int invenCount = 52;
    public GameObject invenObj;
    public GameObject equipSlotPrefab; // 장비슬롯 프리팹
    public GameObject totalSlotPrefab; // 통합인벤 슬롯 프리팹
    public GameObject equipSlotPanel; // 장비슬롯 패널
    public GameObject equipInvenPanel; // 장비인벤토리 패널
    public GameObject totalInvenPanel; // 통합인벤 패널
    public Image equipInvenImage; // 장비인벤토리 패널 상단 이미지
    public ItemDescriptionPanel descriptionPanel; // 아이템 설명 패널
    public SelectPanel selectPanel; // 선택창 패널
    public Image topImage; // 상단패널 이미지
    public TMP_Text topText; // 상단패널 텍스트
    public TMP_Text possessionSoul; // 상단패널 보유소울
    public GameObject goBack; // 바텀 뒤로가기패널

    public List<WeaponSlot> weaponSlotList; // 무기,방패 장착슬롯 리스트
    public List<ArmorSlot> armorSlotList; // 방어구 장착슬롯 리스트
    public List<ConsumptionSlot> consumptionSlotList; // 소모품 장착슬롯 리스트

    public List<ItemData> inventory = new List<ItemData>(); // 인벤토리
    public List<EquipSlot> equipSlots = new List<EquipSlot>(); // 장비인벤 슬롯
    public List<Slot> totalSlots = new List<Slot>(); // 장비인벤 슬롯
    public IPublicSlot selectSlot; // 선택한 슬롯 담을 변수
    public EquipInvenController equipInvenController;
    [SerializeField] private int soul; // 보유소울
    public int Soul
    {
        get { return soul; }
        set
        {
            soul = value;
            possessionSoul.text = soul.ToString();
        }
    }
    //
    public delegate void EventHandler();
    public EventHandler _onEquipSlotUpdated;

    private void Awake()
    {
        _onEquipSlotUpdated = new EventHandler(() => Debug.Log("Equipment Updated"));
    }
    //
    private void Start()
    {
        InitSlot();
        Soul = 0;
    } // Start

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UiManager.Instance.soulBag.GetSoul(5000);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UiManager.Instance.loadingPanel.gameObject.SetActive(!UiManager.Instance.loadingPanel.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.LoadData();
        }
    } // Update

    //! 슬롯 초기화
    public void InitSlot()
    {
        for (int i = 0; i < weaponSlotList.Count; i++)
        {
            weaponSlotList[i].AddItem(null);
        }

        for (int i = 0; i < armorSlotList.Count; i++)
        {
            armorSlotList[i].AddItem(null);
        }

        for (int i = 0; i < consumptionSlotList.Count; i++)
        {
            consumptionSlotList[i].AddItem(null);
        }

        for (int i = 0; i < invenCount; i++)
        {
            // 장비인벤 슬롯 세팅
            GameObject slot = Instantiate(equipSlotPrefab);
            EquipSlot equipSlot = slot.GetComponent<EquipSlot>();
            slot.transform.SetParent(equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform);
            // 통합인벤 슬롯 세팅
            GameObject tSlot = Instantiate(totalSlotPrefab);
            Slot totalSlot = tSlot.GetComponent<Slot>();
            tSlot.transform.SetParent(totalInvenPanel.transform.Find("Scroll View/Viewport/Content").transform);
            equipSlots.Add(equipSlot);
            totalSlots.Add(totalSlot);
            inventory.Add(null);
            equipSlots[i].Item = null;
            totalSlots[i].Item = null;
        }
    } // InitSlot

    //! 인벤토리에 아이템 추가하는 함수
    public void AddItem(ItemData item)
    {
        ItemData itemData = null;
        foreach (string[] _itemData in DataManager.Instance.itemDatas)
        {
            // 아이템데이터 테이블에서 입력받은 ID의 아이템데이터를 가져옴
            if (int.Parse(_itemData[0]) == item.itemID)
            {
                itemData = new ItemData(_itemData);
            }
        }
        //Debug.Log($"인벤에 넣기 전 획득한 아이템 : {itemData.itemName}");

        // 인벤토리에 같은 아이템이 있는지 체크
        foreach (ItemData _item in inventory)
        {
            if (_item != null)
            {
                if (_item.itemID == item.itemID)
                {
                    // 같은 아이템이 있고 보유수량이 최대수량보다 작을 때
                    if (_item.Quantity < itemData.maxQuantity)
                    {
                        // 아이템의 보유수량과 획득한 아이템의 보유수량의 합이 최대보유수량 을 초과할 때
                        if ((_item.Quantity + item.Quantity) > itemData.maxQuantity)
                        {
                            // 최대보유수량으로 나눈 나머지값을 구함
                            int remainQuantity = (_item.Quantity + item.Quantity) % itemData.maxQuantity;
                            // 아이템의 보유수량을 최대수량으로 적용
                            _item.Quantity = itemData.maxQuantity;
                            for (int i = 0; i < inventory.Count; i++)
                            {
                                if (inventory[i] == null || inventory[i].itemType.Equals(ItemType.NONE))
                                {
                                    // 새로운 아이템의 보유수량을 구한 나머지값으로 적용한 후 인벤토리에 넣음
                                    itemData.Quantity = remainQuantity;
                                    inventory[i] = itemData;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            _item.Quantity += item.Quantity;
                        }
                        InitSlotItemData();
                        if (_item.IsEquip == true)
                        {
                            UiManager.Instance.quickSlotBar.LoadQuickSlotData();
                        }
                        return;
                    }
                }
            }
        }

        // 인벤토리에 같은 아이템이 없을 경우
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null || inventory[i].itemType.Equals(ItemType.NONE))
            {
                itemData.Quantity = item.Quantity;
                itemData.IsEquip = item.IsEquip;
                inventory[i] = itemData;
                //Debug.Log($"인벤토리 빈 슬롯에 추가된 아이템 : {inventory[i].itemName}");
                InitSlotItemData();
                return;
            }
        }
        // 인벤토리가 꽉찬 경우 새로운 슬롯 추가
        // 장비인벤 슬롯 추가
        GameObject slot = Instantiate(equipSlotPrefab);
        EquipSlot equipSlot = slot.GetComponent<EquipSlot>();
        slot.transform.parent = equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
        equipSlot.Item = null;
        equipSlots.Add(equipSlot);
        // 통합인벤 슬롯 추가
        GameObject tSlot = Instantiate(totalSlotPrefab);
        Slot totalSlot = tSlot.GetComponent<Slot>();
        tSlot.transform.parent = totalInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
        totalSlot.Item = null;
        totalSlots.Add(totalSlot);
        inventory.Add(item);
        InitSlotItemData();
    } // AddItem

    //! 아이템 버리는 함수
    public void ThrowItem(ItemData itemData)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemData)
            {
                // 버리는 아이템의 프리팹을 인스턴스하고 아이템데이터 대입
                GameObject item = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{itemData.itemID}"));
                item.transform.position = GameManager.Instance.player.gameObject.transform.position + (Vector3.up * 0.3f);
                item.GetComponent<Item>().itemData = itemData;
                inventory[i] = null;
                return;
            }
        }
    } // ThrowItem

    //! 아이템 파괴하는 함수
    public void RemoveItem(ItemData itemData)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemData)
            {
                inventory[i] = null;
                return;
            }
        }
    } // RemoveItem

    //! 정해진 itemType을 장비해야하는 슬롯일 경우 장비인벤슬롯에 같은 itemType인 아이템만 보여주는 함수
    public void InitSameTypeEquipSlot(ItemType _itemType)
    {
        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inventory)
        {
            // 같은 타입의 아이템만 따로 캐싱
            if (_itemType == ItemType.WEAPON || _itemType == ItemType.SHIELD)
            {
                if (_item != null && (_item.itemType == ItemType.WEAPON || _item.itemType == ItemType.SHIELD))
                {
                    sameTypes.Add(_item);
                }
            }
            else if (_item != null && _item.itemType == _itemType)
            {
                sameTypes.Add(_item);
            }
        }

        // itemID 기준으로 오름차순 정렬
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < equipSlots.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // 캐싱해둔 같은 타입의 아이템을 슬롯에 표시
                equipSlots[i].Item = sameTypes[i];
                // 세이브 데이터 로드 시 장비슬롯과 장착슬롯 연동 처리
                if (equipSlots[i].Item.IsEquip == true)
                {
                    switch (equipSlots[i].Item.itemType)
                    {
                        case ItemType.WEAPON:
                            for (int j = 0; j < weaponSlotList.Count; j++)
                            {
                                // 무기슬롯의 아이템이 존재할 경우
                                if (weaponSlotList[j].Item != null)
                                {
                                    // 무기슬롯의 아이템과 장착슬롯의 아이템이 같고 장착슬롯의 아이템이 장착 중일 때
                                    if (weaponSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // 슬롯 연동
                                        equipSlots[i].equipSlot = weaponSlotList[j];
                                        weaponSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.SHIELD:
                            for (int j = 0; j < weaponSlotList.Count; j++)
                            {
                                // 무기슬롯의 아이템이 존재할 경우
                                if (weaponSlotList[j].Item != null)
                                {
                                    // 무기슬롯의 아이템과 장착슬롯의 아이템이 같고 장착슬롯의 아이템이 장착 중일 때
                                    if (weaponSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // 슬롯 연동
                                        equipSlots[i].equipSlot = weaponSlotList[j];
                                        weaponSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.ATTACK_CONSUMPTION:
                            for (int j = 0; j < 3; j++)
                            {
                                // 소모품슬롯의 아이템이 존재할 경우
                                if (consumptionSlotList[j].Item != null)
                                {
                                    // 소모품슬롯의 아이템과 장착슬롯의 아이템이 같고 장착슬롯의 아이템이 장착 중일 때
                                    if (consumptionSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true
                                        && consumptionSlotList[j].Item.Quantity == equipSlots[i].Item.Quantity)
                                    {
                                        // 슬롯 연동
                                        equipSlots[i].equipSlot = consumptionSlotList[j];
                                        consumptionSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.RECOVERY_CONSUMPTION:
                            for (int j = 3; j < consumptionSlotList.Count; j++)
                            {
                                // 소모품슬롯의 아이템이 존재할 경우
                                if (consumptionSlotList[j].Item != null)
                                {
                                    // 소모품슬롯의 아이템과 장착슬롯의 아이템이 같고 장착슬롯의 아이템이 장착 중일 때
                                    if (consumptionSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true
                                        && consumptionSlotList[j].Item.Quantity == equipSlots[i].Item.Quantity)
                                    {
                                        // 슬롯 연동
                                        equipSlots[i].equipSlot = consumptionSlotList[j];
                                        consumptionSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        default:
                            for (int j = 0; j < armorSlotList.Count; j++)
                            {
                                // 방어구슬롯의 아이템이 존재할 경우
                                if (armorSlotList[j].Item != null)
                                {
                                    // 방어구슬롯의 아이템과 장착슬롯의 아이템이 같고 장착슬롯의 아이템이 장착 중일 때
                                    if (armorSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // 슬롯 연동
                                        equipSlots[i].equipSlot = armorSlotList[j];
                                        armorSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                    } // switch
                }
            }
            else
            {
                // 빈곳 표시를 위한 null값
                equipSlots[i].Item = null;
            }
        }
    } // InitEquipInven

    //! 선택한 itemType인 아이템만 통합인벤에 보여주는 함수
    public void InitSameTypeTotalSlot(ItemType _itemType)
    {
        // NONE이면 모든타입의 아이템을 보여줌
        if (_itemType == ItemType.NONE)
        {
            // 기존 인벤토리 크기 캐싱
            int num = inventory.Count;
            // 인벤토리가 비어있지 않을 경우만 itemID 순으로 정렬
            inventory = inventory.Where(x => x != null).OrderBy(x => x.itemID).ToList();
            // null을 제외한 아이템으로 정렬했기 때문에 인벤의 크기가 변경됨 => 기존 크기만큼 나머지 null로 채움
            for (int i = inventory.Count; i < num; i++)
            {
                inventory.Add(null);
            }

            for (int i = 0; i < totalSlots.Count; i++)
            {
                if (inventory[i] != null)
                {
                    // 인벤토리 아이템을 슬롯에 표시
                    totalSlots[i].Item = inventory[i];
                }
                else
                {
                    // 빈곳 표시를 위한 null값
                    totalSlots[i].Item = null;
                }
            }
            return;
        }

        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inventory)
        {
            // 같은 타입의 아이템만 따로 캐싱
            if (_item != null && _item.itemType == _itemType)
            {
                sameTypes.Add(_item);
            }
        }
        //Debug.Log($"{sameTypes.Count}");
        // itemID 기준으로 오름차순 정렬
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < totalSlots.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // 캐싱해둔 같은 타입의 아이템을 슬롯에 표시
                totalSlots[i].Item = sameTypes[i];
            }
            else
            {
                // 빈곳 표시를 위한 null값
                totalSlots[i].Item = null;
            }
        }
    } // InitSameTypeTotalSlot

    //! 장착슬롯 데이터 초기화 함수
    private void InitSlotItemData()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null && inventory[i].IsEquip == true)
            {
                switch (inventory[i].itemType)
                {
                    case ItemType.WEAPON:
                        foreach (var wSlot in weaponSlotList)
                        {
                            if (wSlot.Item != null && wSlot.Item.itemID == inventory[i].itemID)
                            {
                                wSlot.Item = inventory[i];
                                break;
                            }
                        }
                        break;
                    case ItemType.SHIELD:
                        foreach (var wSlot in weaponSlotList)
                        {
                            if (wSlot.Item != null && wSlot.Item.itemID == inventory[i].itemID)
                            {
                                wSlot.Item = inventory[i];
                                break;
                            }
                        }
                        break;
                    case ItemType.ATTACK_CONSUMPTION:
                        for (int j = 0; j < 3; j++)
                        {
                            if (consumptionSlotList[j].Item != null)
                            {
                                if (consumptionSlotList[j].Item.itemID == inventory[i].itemID)
                                {
                                    consumptionSlotList[j].Item = inventory[i];
                                    break;
                                }
                            }
                        }
                        break;
                    case ItemType.RECOVERY_CONSUMPTION:
                        for (int j = 3; j < consumptionSlotList.Count; j++)
                        {
                            if (consumptionSlotList[j].Item != null)
                            {
                                if (consumptionSlotList[j].Item.itemID == inventory[i].itemID)
                                {
                                    consumptionSlotList[j].Item = inventory[i];
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        foreach (var aSlot in armorSlotList)
                        {
                            if (aSlot.Item != null && aSlot.Item.itemID == inventory[i].itemID)
                            {
                                aSlot.Item = inventory[i];
                                break;
                            }
                        }
                        break;
                } // switch
            } // if
        } // for
    } // InitSlotItemData
} // Inventory
