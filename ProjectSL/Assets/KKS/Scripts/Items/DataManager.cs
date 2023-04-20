using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // 아이템 데이터 리스트
    private string path;
    private string wSlot = "무기슬롯";
    private string aSlot = "방어구슬롯";
    private string cSlot = "소모품슬롯";
    public override void InitManager()
    {
        StartCoroutine(GoogleSheetManager.InitData());
        path = "C:/unitykks/ProjectSL" + "/SavePlayerData";
    } // InitManager

    //! 플레이어 데이터 세이브하는 함수
    public void SaveData()
    {
        string saveData = null;
        saveData = SaveInventoryData();
        saveData += SaveEquipSlotData();
        Debug.Log(saveData);
        File.WriteAllText(path, saveData);
    } // SaveData

    //! 인벤토리 데이터 저장하는 함수
    private string SaveInventoryData()
    {
        string data = null;
        string saveData = null;
        foreach (var item in Inventory.Instance.inventory)
        {
            // 인벤토리안의 아이템들을 Json파일로 저장 아이템 구분: \n 사용
            data += JsonUtility.ToJson(item) + "\n";
        }
        // 저장된 Json파일의 데이터를 \n 기준으로 잘라서 배열에 저장
        string[] datas = data.Split('\n');
        Debug.Log(datas.Length);
        foreach (var _data in datas)
        {
            bool isNullData = true;
            if (!string.IsNullOrWhiteSpace(_data))
            {
                isNullData = false;
            }

            // 데이터의 값이 비어있지 않을 경우만 saveData에 저장
            if (isNullData == false)
            {
                // 아이템 순서 구분을 위한 \n을 더해서 저장
                saveData += _data + "\n";
            }
        }
        return saveData;
    } // SaveInventoryData

    private string SaveEquipSlotData()
    {
        // 무기슬롯의 장착된 아이템 정보 저장
        string SaveEquipSlotData = wSlot + "\n";
        List<WeaponSlot> wSlotList = Inventory.Instance.weaponSlotList;
        for (int i = 0; i < wSlotList.Count; i++)
        {
            if (wSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "번";
                SaveEquipSlotData += JsonUtility.ToJson(wSlotList[i].Item) + "\n";
            }
        }

        // 방어구슬롯의 장착된 아이템 정보 저장
        SaveEquipSlotData += aSlot + "\n";
        List<ArmorSlot> aSlotList = Inventory.Instance.armorSlotList;
        for (int i = 0; i < aSlotList.Count; i++)
        {
            if (aSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "번";
                SaveEquipSlotData += JsonUtility.ToJson(aSlotList[i].Item) + "\n";
            }
        }

        // 소모품슬롯의 장착된 아이템 정보 저장
        SaveEquipSlotData += cSlot + "\n";
        List<ConsumptionSlot> cSlotList = Inventory.Instance.consumptionSlotList;
        for (int i = 0; i < cSlotList.Count; i++)
        {
            if (cSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "번";
                SaveEquipSlotData += JsonUtility.ToJson(cSlotList[i].Item) + "\n";
            }
        }
        return SaveEquipSlotData;
    } // SaveEquipSlotData

    //! 세이브데이터 로드하는 함수
    public void LoadData()
    {
        List<string> newItemDatas = new List<string>();
        // 저장된 Json파일을 불러옴
        string data = File.ReadAllText(path);
        TextAsset inventoryData = new TextAsset(data);
        // \n을 기준으로 잘라서 배열에 데이터 저장
        string[] itemDatas = inventoryData.text.Split("\n");
        int number = 0;
        Debug.Log($"불러온 세이브데이터의 길이 : {itemDatas.Length}");
        for (int i = 0; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == wSlot)
            {
                Debug.Log("무기슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            ItemData item = JsonUtility.FromJson<ItemData>(itemDatas[i]);
            Debug.Log($"{i}번째 아이템 : {item.itemName}");
            Inventory.Instance.AddItem(item);
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == aSlot)
            {
                Debug.Log("방어구슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            string[] wSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{wSlotData.Length} 인덱스 : {wSlotData[0]} 아이템 : {wSlotData[1]}");
            int wSlotIndex = int.Parse(wSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(wSlotData[1]);
            //Debug.Log($"아이템데이터 타입으로 변환시킨 정보 : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.weaponSlotList[wSlotIndex].Item = _item;
                    break;
                }
            }
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            if (itemDatas[i] == cSlot)
            {
                Debug.Log("소모품슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            string[] aSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{aSlotData.Length} 인덱스 : {aSlotData[0]} 아이템 : {aSlotData[1]}");
            int aSlotIndex = int.Parse(aSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(aSlotData[1]);
            //Debug.Log($"아이템데이터 타입으로 변환시킨 정보 : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.armorSlotList[aSlotIndex].Item = _item;
                    break;
                }
            }
        }

        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            string[] cSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{cSlotData.Length} 인덱스 : {cSlotData[0]} 아이템 : {cSlotData[1]}");
            int cSlotIndex = int.Parse(cSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(cSlotData[1]);
            //Debug.Log($"아이템데이터 타입으로 변환시킨 정보 : {item} {item.itemName}");
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                if (_item.itemID == item.itemID)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.consumptionSlotList[cSlotIndex].Item = _item;
                    break;
                }
            }
        }
    } // LoadData
} // DataManager