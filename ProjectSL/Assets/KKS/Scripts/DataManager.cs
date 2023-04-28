using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // ������ ������ ����Ʈ
    //private string path = "C:/unitykks/ProjectSL/SaveData"; // ������ ���� ���
    private string path; // ������ ���� ���
    public int slotNum; // ���̺� ���� �ѹ�
    public bool[] hasSavefile; // ���̺� ������ ������ ���� ����
    private string playerData = "�÷��̾� ����"; // json ������ �Ľ��� �� ���� ������
    private string wSlot = "���⽽��"; // json ������ �Ľ��� �� ���� ������
    private string aSlot = "������"; // json ������ �Ľ��� �� ���� ������
    private string cSlot = "�Ҹ�ǰ����"; // json ������ �Ľ��� �� ���� ������
    private string qSlot = "������ �޼�/������/���ݼҸ�ǰ/ȸ���Ҹ�ǰ"; // json ������ �Ľ��� �� ���� ������
    private string bonfire = "ȭ��Ҹ���Ʈ"; // json ������ �Ľ��� �� ���� ������
    public override void InitManager()
    {
        // ������ ���� ��� ����
        path = Application.dataPath + "/SaveFolder/";
        StartCoroutine(GoogleSheetManager.InitData());
        hasSavefile = new bool[4];
        for (int i = 0; i < hasSavefile.Length; i++)
        {
            // ���̺� ������ ������ ������ ��� true
            if (File.Exists(path + $"{i}") == true)
            {
                hasSavefile[i] = true;
            }
        }
    } // InitManager

    //! �÷��̾� ������ ���̺��ϴ� �Լ�
    public void SaveData()
    {
        string saveData = null;
        saveData = SaveInventoryData();
        saveData += SaveEquipSlotData();
        saveData += SaveBonfireList();
        saveData += SavePlayerData();
        Debug.Log(saveData);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "SaveData" + slotNum.ToString() + ".json", saveData);
    } // SaveData

    //! �÷��̾� ������ �����ϴ� �Լ�
    private string SavePlayerData()
    {
        string saveData = playerData + "\n";
        PlayerStatus _playerStatus = GameManager.Instance.player.GetPlayerData();
        saveData += JsonUtility.ToJson(_playerStatus) + "\n";
        return saveData;
    } // GetPlayerData.

    //! �κ��丮 ������ �����ϴ� �Լ�
    private string SaveInventoryData()
    {
        string data = null;
        string saveData = null;
        foreach (ItemData item in Inventory.Instance.inventory)
        {
            // �κ��丮���� �����۵��� Json���Ϸ� ���� ������ ����: \n ���
            data += JsonUtility.ToJson(item) + "\n";
        }
        // ����� Json������ �����͸� \n �������� �߶� �迭�� ����
        string[] datas = data.Split('\n');
        Debug.Log(datas.Length);
        foreach (string _data in datas)
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

    //! ��� ������ ������ �����ϴ� �Լ�
    private string SaveEquipSlotData()
    {
        // ���⽽���� ������ ������ ���� ���� wSlot���� ����
        string SaveEquipSlotData = wSlot + "\n";
        List<WeaponSlot> wSlotList = Inventory.Instance.weaponSlotList;
        for (int i = 0; i < wSlotList.Count; i++)
        {
            // ���⽽���� �������� ���� ���
            if (wSlotList[i].Item != null)
            {
                // JsonUtility�� ��ųʸ��� ���������ʾ� "��"�� �������� Ű���� �������� �����ؼ� ������
                SaveEquipSlotData += $"{i}" + "��";
                SaveEquipSlotData += JsonUtility.ToJson(wSlotList[i].Item) + "\n";
            }
        }

        // �������� ������ ������ ���� ���� aSlot���� ����
        SaveEquipSlotData += aSlot + "\n";
        List<ArmorSlot> aSlotList = Inventory.Instance.armorSlotList;
        for (int i = 0; i < aSlotList.Count; i++)
        {
            // �������� �������� ���� ���
            if (aSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "��";
                SaveEquipSlotData += JsonUtility.ToJson(aSlotList[i].Item) + "\n";
            }
        }

        // �Ҹ�ǰ������ ������ ������ ���� ���� cSlot���� ����
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

        // ������ ������ ���� qSlot���� ����
        SaveEquipSlotData += qSlot + "\n";
        SaveEquipSlotData += UiManager.Instance.quickSlotBar.leftArmNum.ToString() + "/" +
            UiManager.Instance.quickSlotBar.rightArmNum.ToString() + "/" +
            UiManager.Instance.quickSlotBar.attackC_Num.ToString() + "/" +
            UiManager.Instance.quickSlotBar.recoveryC_Num.ToString() + "\n";
        return SaveEquipSlotData;
    } // SaveEquipSlotData

    //! ȭ��� ����Ʈ �����ϴ� �Լ�
    private string SaveBonfireList()
    {
        string bonfireData = bonfire + "\n";
        foreach (BonfireData bonfire in UiManager.Instance.warp.bonfireList)
        {
            bonfireData += JsonUtility.ToJson(bonfire) + "\n";
        }
        return bonfireData;
    } // SaveBonfireList

    //! ���̺굥���� �ε��ϴ� �Լ�
    public void LoadData()
    {
        // ������ �ε��� �κ��丮 �ʱ�ȭ
        for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
        {
            Inventory.Instance.inventory[i] = null;
        }
        List<string> newItemDatas = new List<string>();
        // ����� Json������ �ҷ���
        string data = File.ReadAllText(path + "SaveData" + slotNum.ToString() + ".json");
        TextAsset inventoryData = new TextAsset(data);
        // \n�� �������� �߶� �迭�� ������ ����
        string[] itemDatas = inventoryData.text.Split("\n");
        int number = 0;
        //Debug.Log($"�ҷ��� ���̺굥������ ���� : {itemDatas.Length}");

        // ���� �κ��丮 ������ �ε�
        for (int i = 0; i < itemDatas.Length - 1; i++)
        {
            // wSlot ���� ������ �� ���� �ٺ��� ���⽽�� ������
            if (itemDatas[i] == wSlot)
            {
                //Debug.Log("���⽽�� ������ ����");
                number = i + 1;
                break;
            }
            // ���� �κ��丮�� ������ �Ľ�
            ItemData item = JsonUtility.FromJson<ItemData>(itemDatas[i]);
            //Debug.Log($"{i}��° ������ : {item.itemName}");
            Inventory.Instance.AddItem(item);
        }

        // ���� ���� ���� ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // aSlot ���� ������ �� ���� �ٺ��� ������ ������
            if (itemDatas[i] == aSlot)
            {
                //Debug.Log("������ ������ ����");
                number = i + 1;
                break;
            }
            // JsonUtility�� ��ųʸ��� ���������ʾ� "��"�� �������� Ű���� �������� �����ؼ� �Ľ���
            string[] wSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{wSlotData.Length} �ε��� : {wSlotData[0]} ������ : {wSlotData[1]}");
            int wSlotIndex = int.Parse(wSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(wSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // �ε��� ���� �κ��丮���� �������� ���� �������� ã�Ƽ� ���⽽�Կ� ���
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.weaponSlotList[wSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // �� ���� ���� ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // cSlot�� ���� ������ �� ���� �ٺ��� �Ҹ�ǰ���� ������
            if (itemDatas[i] == cSlot)
            {
                //Debug.Log("�Ҹ�ǰ���� ������ ����");
                number = i + 1;
                break;
            }
            string[] aSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{aSlotData.Length} �ε��� : {aSlotData[0]} ������ : {aSlotData[1]}");
            int aSlotIndex = int.Parse(aSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(aSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // �ε��� ���� �κ��丮���� �������� ���� �������� ã�Ƽ� �����Կ� ���
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.armorSlotList[aSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // �Ҹ�ǰ ���� ���� ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // qSlot�� ���� ������ �� ���� �ٺ��� ������ ������
            if (itemDatas[i] == qSlot)
            {
                number = i + 1;
                break;
            }
            string[] cSlotData = itemDatas[i].Split("��");
            //Debug.Log($"{cSlotData.Length} �ε��� : {cSlotData[0]} ������ : {cSlotData[1]}");
            int cSlotIndex = int.Parse(cSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(cSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // �ε��� ���� �κ��丮���� �������� ���� �������� ã�Ƽ� �Ҹ�ǰ���Կ� ���
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"�κ��� �����۰� ������ ������ �߰� {_item.itemID} / {item.itemID}");
                    Inventory.Instance.consumptionSlotList[cSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // ������ ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // bonfire�� ���� ������ �� ���� �ٺ��� ȭ��� ������
            if (itemDatas[i] == bonfire)
            {
                number = i + 1;
                break;
            }
            string[] qSlotDatas = itemDatas[i].Split("/");
            UiManager.Instance.quickSlotBar.leftArmNum = int.Parse(qSlotDatas[0]);
            UiManager.Instance.quickSlotBar.rightArmNum = int.Parse(qSlotDatas[1]);
            UiManager.Instance.quickSlotBar.attackC_Num = int.Parse(qSlotDatas[2]);
            UiManager.Instance.quickSlotBar.recoveryC_Num = int.Parse(qSlotDatas[3]);
            UiManager.Instance.quickSlotBar.LoadQuickSlotData();
        }

        //! ȭ��� ����Ʈ ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // playerData�� ���� ������ �� ���� �ٺ��� �÷��̾� ������
            if (itemDatas[i] == playerData)
            {
                number = i + 1;
                break;
            }
            BonfireData bonfire = JsonUtility.FromJson<BonfireData>(itemDatas[i]);
            // bonfireList�� �ҷ��� ȭ��ҵ������� �̸��� ������ �̸��� �������� �ʴ� ��쿡�� add �� slot����
            if (!UiManager.Instance.warp.bonfireList.Any(b => b.bonfireName == bonfire.bonfireName))
            {
                UiManager.Instance.warp.bonfireList.Add(bonfire);
                UiManager.Instance.warp.CreateWarpSlot(bonfire);
            }
        }

        //! �÷��̾� ������ �ε�
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            PlayerStatus playerStatus = JsonUtility.FromJson<PlayerStatus>(itemDatas[i]);
            GameManager.Instance.player.LoadPlayerData(playerStatus);
        }
        Debug.Log("����� ������ �ε� �Ϸ�!");
    } // LoadData
} // DataManager