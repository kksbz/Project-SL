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
    public GameObject equipSlotPrefab; // ��񽽷� ������
    public GameObject totalSlotPrefab; // �����κ� ���� ������
    public GameObject equipSlotPanel; // ��񽽷� �г�
    public GameObject equipInvenPanel; // ����κ��丮 �г�
    public GameObject totalInvenPanel; // �����κ� �г�
    public Image equipInvenImage; // ����κ��丮 �г� ��� �̹���
    public ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public SelectPanel selectPanel; // ����â �г�
    public Image topImage; // ����г� �̹���
    public TMP_Text topText; // ����г� �ؽ�Ʈ
    public TMP_Text possessionSoul; // ����г� �����ҿ�
    public GameObject goBack; // ���� �ڷΰ����г�

    public List<WeaponSlot> weaponSlotList; // ����,���� �������� ����Ʈ
    public List<ArmorSlot> armorSlotList; // �� �������� ����Ʈ
    public List<ConsumptionSlot> consumptionSlotList; // �Ҹ�ǰ �������� ����Ʈ

    public List<ItemData> inventory = new List<ItemData>(); // �κ��丮
    public List<EquipSlot> equipSlots = new List<EquipSlot>(); // ����κ� ����
    public List<Slot> totalSlots = new List<Slot>(); // ����κ� ����
    public IPublicSlot selectSlot; // ������ ���� ���� ����
    public EquipInvenController equipInvenController;
    [SerializeField] private int soul; // �����ҿ�
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
    public EventHandler _onEquipArmorUpdated;

    public override void InitManager()
    {
        InitSlot();
        Soul = 0;
        _onEquipSlotUpdated = new EventHandler(() => Debug.Log("Equipment Updated"));
        _onEquipArmorUpdated = new EventHandler(() => Debug.Log("Armor Updated"));
    }
    //

    //! ���� �ʱ�ȭ
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
            // ����κ� ���� ����
            GameObject slot = Instantiate(equipSlotPrefab);
            EquipSlot equipSlot = slot.GetComponent<EquipSlot>();
            slot.transform.SetParent(equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform);
            // �����κ� ���� ����
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

    //! �κ��丮�� ������ �߰��ϴ� �Լ�
    public void AddItem(ItemData item)
    {
        ItemData itemData = null;
        foreach (string[] _itemData in DataManager.Instance.itemDatas)
        {
            // �����۵����� ���̺����� �Է¹��� ID�� �����۵����͸� ������
            if (int.Parse(_itemData[0]) == item.itemID)
            {
                itemData = new ItemData(_itemData);
            }
        }
        //Debug.Log($"�κ��� �ֱ� �� ȹ���� ������ : {itemData.itemName}");

        // �κ��丮�� ���� �������� �ִ��� üũ
        foreach (ItemData _item in inventory)
        {
            if (_item != null)
            {
                if (_item.itemID == item.itemID)
                {
                    // ���� �������� �ְ� ���������� �ִ�������� ���� ��
                    if (_item.Quantity < itemData.maxQuantity)
                    {
                        // �������� ���������� ȹ���� �������� ���������� ���� �ִ뺸������ �� �ʰ��� ��
                        if ((_item.Quantity + item.Quantity) > itemData.maxQuantity)
                        {
                            // �ִ뺸���������� ���� ���������� ����
                            int remainQuantity = (_item.Quantity + item.Quantity) % itemData.maxQuantity;
                            // �������� ���������� �ִ�������� ����
                            _item.Quantity = itemData.maxQuantity;
                            for (int i = 0; i < inventory.Count; i++)
                            {
                                if (inventory[i] == null || inventory[i].itemType.Equals(ItemType.NONE))
                                {
                                    // ���ο� �������� ���������� ���� ������������ ������ �� �κ��丮�� ����
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

        // �κ��丮�� ���� �������� ���� ���
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null || inventory[i].itemType.Equals(ItemType.NONE))
            {
                itemData.Quantity = item.Quantity;
                itemData.IsEquip = item.IsEquip;
                inventory[i] = itemData;
                //Debug.Log($"�κ��丮 �� ���Կ� �߰��� ������ : {inventory[i].itemName}");
                InitSlotItemData();
                return;
            }
        }
        // �κ��丮�� ���� ��� ���ο� ���� �߰�
        // ����κ� ���� �߰�
        GameObject slot = Instantiate(equipSlotPrefab);
        EquipSlot equipSlot = slot.GetComponent<EquipSlot>();
        slot.transform.parent = equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
        equipSlot.Item = null;
        equipSlots.Add(equipSlot);
        // �����κ� ���� �߰�
        GameObject tSlot = Instantiate(totalSlotPrefab);
        Slot totalSlot = tSlot.GetComponent<Slot>();
        tSlot.transform.parent = totalInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
        totalSlot.Item = null;
        totalSlots.Add(totalSlot);
        inventory.Add(item);
        InitSlotItemData();
    } // AddItem

    //! ������ ������ �Լ�
    public void ThrowItem(ItemData itemData)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemData)
            {
                // ������ �������� �������� �ν��Ͻ��ϰ� �����۵����� ����
                GameObject item = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{itemData.itemID}"));
                item.transform.position = GameManager.Instance.player.gameObject.transform.position + (Vector3.up * 0.3f);
                item.GetComponent<Item>().itemData = itemData;
                inventory[i] = null;
                return;
            }
        }
    } // ThrowItem

    //! ������ �ı��ϴ� �Լ�
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

    //! ������ itemType�� ����ؾ��ϴ� ������ ��� ����κ����Կ� ���� itemType�� �����۸� �����ִ� �Լ�
    public void InitSameTypeEquipSlot(ItemType _itemType)
    {
        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inventory)
        {
            // ���� Ÿ���� �����۸� ���� ĳ��
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

        // itemID �������� �������� ����
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < equipSlots.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // ĳ���ص� ���� Ÿ���� �������� ���Կ� ǥ��
                equipSlots[i].Item = sameTypes[i];
                // ���̺� ������ �ε� �� ��񽽷԰� �������� ���� ó��
                if (equipSlots[i].Item.IsEquip == true)
                {
                    switch (equipSlots[i].Item.itemType)
                    {
                        case ItemType.WEAPON:
                            for (int j = 0; j < weaponSlotList.Count; j++)
                            {
                                // ���⽽���� �������� ������ ���
                                if (weaponSlotList[j].Item != null)
                                {
                                    // ���⽽���� �����۰� ���������� �������� ���� ���������� �������� ���� ���� ��
                                    if (weaponSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // ���� ����
                                        equipSlots[i].equipSlot = weaponSlotList[j];
                                        weaponSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.SHIELD:
                            for (int j = 0; j < weaponSlotList.Count; j++)
                            {
                                // ���⽽���� �������� ������ ���
                                if (weaponSlotList[j].Item != null)
                                {
                                    // ���⽽���� �����۰� ���������� �������� ���� ���������� �������� ���� ���� ��
                                    if (weaponSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // ���� ����
                                        equipSlots[i].equipSlot = weaponSlotList[j];
                                        weaponSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.ATTACK_CONSUMPTION:
                            for (int j = 0; j < 3; j++)
                            {
                                // �Ҹ�ǰ������ �������� ������ ���
                                if (consumptionSlotList[j].Item != null)
                                {
                                    // �Ҹ�ǰ������ �����۰� ���������� �������� ���� ���������� �������� ���� ���� ��
                                    if (consumptionSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true
                                        && consumptionSlotList[j].Item.Quantity == equipSlots[i].Item.Quantity)
                                    {
                                        // ���� ����
                                        equipSlots[i].equipSlot = consumptionSlotList[j];
                                        consumptionSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        case ItemType.RECOVERY_CONSUMPTION:
                            for (int j = 3; j < consumptionSlotList.Count; j++)
                            {
                                // �Ҹ�ǰ������ �������� ������ ���
                                if (consumptionSlotList[j].Item != null)
                                {
                                    // �Ҹ�ǰ������ �����۰� ���������� �������� ���� ���������� �������� ���� ���� ��
                                    if (consumptionSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true
                                        && consumptionSlotList[j].Item.Quantity == equipSlots[i].Item.Quantity)
                                    {
                                        // ���� ����
                                        equipSlots[i].equipSlot = consumptionSlotList[j];
                                        consumptionSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            break;
                        default:
                            for (int j = 0; j < armorSlotList.Count; j++)
                            {
                                // �������� �������� ������ ���
                                if (armorSlotList[j].Item != null)
                                {
                                    // �������� �����۰� ���������� �������� ���� ���������� �������� ���� ���� ��
                                    if (armorSlotList[j].Item.itemID == equipSlots[i].Item.itemID
                                        && equipSlots[i].Item.IsEquip == true)
                                    {
                                        // ���� ����
                                        equipSlots[i].equipSlot = armorSlotList[j];
                                        armorSlotList[j].Item = equipSlots[i].Item;
                                    }
                                }
                            }
                            //_onEquipArmorUpdated();
                            break;
                    } // switch
                }
            }
            else
            {
                // ��� ǥ�ø� ���� null��
                equipSlots[i].Item = null;
            }
        }
    } // InitEquipInven

    //! ������ itemType�� �����۸� �����κ��� �����ִ� �Լ�
    public void InitSameTypeTotalSlot(ItemType _itemType)
    {
        // NONE�̸� ���Ÿ���� �������� ������
        if (_itemType == ItemType.NONE)
        {
            // ���� �κ��丮 ũ�� ĳ��
            int num = inventory.Count;
            // �κ��丮�� ������� ���� ��츸 itemID ������ ����
            inventory = inventory.Where(x => x != null).OrderBy(x => x.itemID).ToList();
            // null�� ������ ���������� �����߱� ������ �κ��� ũ�Ⱑ ����� => ���� ũ�⸸ŭ ������ null�� ä��
            for (int i = inventory.Count; i < num; i++)
            {
                inventory.Add(null);
            }

            for (int i = 0; i < totalSlots.Count; i++)
            {
                if (inventory[i] != null)
                {
                    // �κ��丮 �������� ���Կ� ǥ��
                    totalSlots[i].Item = inventory[i];
                }
                else
                {
                    // ��� ǥ�ø� ���� null��
                    totalSlots[i].Item = null;
                }
            }
            return;
        }

        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inventory)
        {
            // ���� Ÿ���� �����۸� ���� ĳ��
            if (_item != null && _item.itemType == _itemType)
            {
                sameTypes.Add(_item);
            }
        }
        //Debug.Log($"{sameTypes.Count}");
        // itemID �������� �������� ����
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < totalSlots.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // ĳ���ص� ���� Ÿ���� �������� ���Կ� ǥ��
                totalSlots[i].Item = sameTypes[i];
            }
            else
            {
                // ��� ǥ�ø� ���� null��
                totalSlots[i].Item = null;
            }
        }
    } // InitSameTypeTotalSlot

    //! �������� ������ �ʱ�ȭ �Լ�
    public void InitSlotItemData()
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
