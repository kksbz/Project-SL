using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static ItemData;
using static UnityEditor.Progress;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private GameObject invenObj;
    private int invenCount = 52;
    public GameObject equipSlotPrefab; // ��񽽷� ������
    public GameObject totalSlotPrefab; // �����κ� ���� ������
    public GameObject equipSlotPanel; // ��񽽷� �г�
    public GameObject equipInvenPanel; // ����κ��丮 �г�
    public GameObject totalInvenPanel; // �����κ� �г�
    public TMP_Text equipInvenText; // ����κ��丮 �г� ��� �ؽ�Ʈ
    public TMP_Text totalInvenText; // �����κ��丮 �г� ��� �ؽ�Ʈ
    public ItemDescriptionPanel descriptionPanel; // ������ ���� �г�
    public SelectPanel selectPanel; // ����â �г�

    public List<WeaponSlot> weaponSlotList;
    public List<ArmorSlot> armorSlotList;
    public List<ConsumptionSlot> consumptionSlotList;

    public List<ItemData> inventory = new List<ItemData>(); // �κ��丮
    public List<EquipSlot> equipSlots = new List<EquipSlot>(); // ����κ� ����
    public List<Slot> totalSlots = new List<Slot>(); // ����κ� ����
    public IPublicSlot selectSlot; // ������ ���� ���� ����

    private void Start()
    {
        InitSlot();
    } // Start

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // ����κ� ���� ����
            GameObject slot = Instantiate(equipSlotPrefab);
            EquipSlot equipSlot = slot.GetComponent<EquipSlot>();
            slot.transform.parent = equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
            equipSlot.Item = null;
            // �����κ� ���� ����
            GameObject tSlot = Instantiate(totalSlotPrefab);
            Slot totalSlot = tSlot.GetComponent<Slot>();
            tSlot.transform.parent = totalInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
            totalSlot.Item = null;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            invenObj.SetActive(false);
            equipSlotPanel.SetActive(true);
            equipInvenPanel.SetActive(false);
            selectPanel.gameObject.SetActive(false);
            totalInvenPanel.SetActive(false);
        }
    } // Update

    //! ���� �ʱ�ȭ
    private void InitSlot()
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
            slot.transform.parent = equipInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
            // �����κ� ���� ����
            GameObject tSlot = Instantiate(totalSlotPrefab);
            Slot totalSlot = tSlot.GetComponent<Slot>();
            tSlot.transform.parent = totalInvenPanel.transform.Find("Scroll View/Viewport/Content").transform;
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
        ItemData itemData = new ItemData(DataManager.Instance.itemDatas[item.itemID - 1]);
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
                        _item.Quantity++;
                        return;
                    }
                }
            }
        }
        Debug.Log($"��ȹ���߼�");
        // �κ��丮�� ���� �������� ���� ���
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log($"{inventory[i].itemName}");
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
    } // AddItem

    //! ������ ������ �Լ�
    public void ThrowItem(ItemData item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                return;
            }
        }
    } // ThrowItem

    //! ������ �ı��ϴ� �Լ�
    public void RemoveItem(ItemData item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                return;
            }
        }
    } // RemoveItem

    //! ������ itemType�� ����ؾ��ϴ� ������ ��� ����κ����Կ� ���� itemType�� �����۸� �����ִ� �Լ�
    public void InitSameTypeEquipSlot(ItemType _itemType)
    {
        Debug.Log(_itemType);
        List<ItemData> sameTypes = new List<ItemData>();
        foreach (ItemData _item in inventory)
        {
            // ���� Ÿ���� �����۸� ���� ĳ��
            if (_item != null && _item.itemType == _itemType)
            {
                sameTypes.Add(_item);
            }
        }
        for (int i = 0; i < sameTypes.Count; i++)
        {
            Debug.Log($"{sameTypes.Count}, {sameTypes[i].itemName}, {sameTypes[i].Quantity}");
        }
        // itemID �������� �������� ����
        sameTypes = sameTypes.OrderBy(x => x.itemID).ToList();
        for (int i = 0; i < equipSlots.Count; i++)
        {
            if (i < sameTypes.Count)
            {
                // ĳ���ص� ���� Ÿ���� �������� ���Կ� ǥ��
                equipSlots[i].Item = sameTypes[i];
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
            // itemID ������ ����
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
        Debug.Log($"{sameTypes.Count}");
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
    } // InitEquipInven
} // Inventory
