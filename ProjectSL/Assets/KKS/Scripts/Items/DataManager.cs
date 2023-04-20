using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // ������ ������ ����Ʈ
    private string path;
    private string wSlot = "���⽽��";
    private string aSlot = "������";
    private string cSlot = "�Ҹ�ǰ����";
    public override void InitManager()
    {
        StartCoroutine(GoogleSheetManager.InitData());
        path = "C:/unitykks/ProjectSL" + "/SavePlayerData";
    } // InitManager

    //! �÷��̾� ������ ���̺��ϴ� �Լ�
    public void SaveData()
    {
        string saveData = null;
        saveData = SaveInventoryData();
        saveData += SaveEquipSlotData();
        Debug.Log(saveData);
        File.WriteAllText(path, saveData);
    } // SaveData

    //! �κ��丮 ������ �����ϴ� �Լ�
    private string SaveInventoryData()
    {
        string data = null;
        string saveData = null;
        foreach (var item in Inventory.Instance.inventory)
        {
            // �κ��丮���� �����۵��� Json���Ϸ� ���� ������ ����: \n ���
            data += JsonUtility.ToJson(item) + "\n";
        }
        // ����� Json������ �����͸� \n �������� �߶� �迭�� ����
        string[] datas = data.Split('\n');
        Debug.Log(datas.Length);
        foreach (var _data in datas)
        {
            bool isNullData = true;
            if (!string.IsNullOrWhiteSpace(_data))
            {
                isNullData = false;
            }

            // �������� ���� ������� ���� ��츸 saveData�� ����
            if (isNullData == false)
            {
                // ������ ���� ������ ���� \n�� ���ؼ� ����
                saveData += _data + "\n";
            }
        }
        return saveData;
    } // SaveInventoryData

    private string SaveEquipSlotData()
    {
        // ���⽽���� ������ ������ ���� ����
        string SaveEquipSlotData = wSlot + "\n";
        List<WeaponSlot> wSlotList = Inventory.Instance.weaponSlotList;
        for (int i = 0; i < wSlotList.Count; i++)
        {
            if (wSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "��";
                SaveEquipSlotData += JsonUtility.ToJson(wSlotList[i].Item) + "\n";
            }
        }

        // �������� ������ ������ ���� ����
        SaveEquipSlotData += aSlot + "\n";
        List<ArmorSlot> aSlotList = Inventory.Instance.armorSlotList;
        for (int i = 0; i < aSlotList.Count; i++)
        {
            if (aSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "��";
                SaveEquipSlotData += JsonUtility.ToJson(aSlotList[i].Item) + "\n";
            }
        }

        // �Ҹ�ǰ������ ������ ������ ���� ����
        SaveEquipSlotData += cSlot + "\n";
        List<ConsumptionSlot> cSlotList = Inventory.Instance.consumptionSlotList;
        for (int i = 0; i < cSlotList.Count; i++)
        {
            if (cSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "��";
                SaveEquipSlotData += JsonUtility.ToJson(cSlotList[i].Item) + "\n";
            }
        }
        return SaveEquipSlotData;
    } // SaveEquipSlotData

    //! ���̺굥���� �ε��ϴ� �Լ�
    public void LoadData()
    {
        List<string> newItemDatas = new List<string>();
        // ����� Json������ �ҷ���
        string data = File.ReadAllText(path);
        TextAsset inventoryData = new TextAsset(data);
        // \n�� �������� �߶� �迭�� ������ ����
        string[] itemDatas = inventoryData.text.Split("\n");
        int number = 0;
        Debug.Log($"�ҷ��� ���̺굥������ ���� : {itemDatas.Length}");
        for (int i = 0; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == wSlot)
            {
                Debug.Log("���⽽�� ������ ����");
                number = i + 1;
                break;
            }
            ItemData item = JsonUtility.FromJson<ItemData>(itemDatas[i]);
            Debug.Log($"{i}��° ������ : {item.itemName}");
            Inventory.Instance.AddItem(item);
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == aSlot)
            {
                Debug.Log("������ ������ ����");
                number = i + 1;
                break;
            }
            string[] wSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{wSlotData.Length} �ε��� : {wSlotData[0]} ������ : {wSlotData[1]}");
            int wSlotIndex = int.Parse(wSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(wSlotData[1]);
            //Debug.Log($"�����۵����� Ÿ������ ��ȯ��Ų ���� : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.weaponSlotList[wSlotIndex].Item = _item;
                    break;
                }
            }
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == cSlot)
            {
                Debug.Log("�Ҹ�ǰ���� ������ ����");
                number = i + 1;
                break;
            }
            string[] aSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{aSlotData.Length} �ε��� : {aSlotData[0]} ������ : {aSlotData[1]}");
            int aSlotIndex = int.Parse(aSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(aSlotData[1]);
            //Debug.Log($"�����۵����� Ÿ������ ��ȯ��Ų ���� : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.armorSlotList[aSlotIndex].Item = _item;
                    break;
                }
            }
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            string[] cSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{cSlotData.Length} �ε��� : {cSlotData[0]} ������ : {cSlotData[1]}");
            int cSlotIndex = int.Parse(cSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(cSlotData[1]);
            //Debug.Log($"�����۵����� Ÿ������ ��ȯ��Ų ���� : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.consumptionSlotList[cSlotIndex].Item = _item;
                    break;
                }
            }
        }
    } // LoadData
} // DataManager