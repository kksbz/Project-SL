using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // 아이템 데이터 리스트
    //private string path = "C:/unitykks/ProjectSL/SaveData"; // 데이터 저장 경로
    private string path; // 데이터 저장 경로
    public int slotNum; // 세이브 슬롯 넘버
    public bool[] hasSavefile; // 세이브 슬롯의 데이터 존재 유무
    private string wSlot = "무기슬롯"; // json 데이터 파싱할 때 슬롯 구분자
    private string aSlot = "방어구슬롯"; // json 데이터 파싱할 때 슬롯 구분자
    private string cSlot = "소모품슬롯"; // json 데이터 파싱할 때 슬롯 구분자
    private string qSlot = "퀵슬롯 왼손/오른손/공격소모품/회복소모품"; // json 데이터 파싱할 때 슬롯 구분자
    private string bonfire = "화톳불리스트"; // json 데이터 파싱할 때 슬롯 구분자
    public override void InitManager()
    {
        // 데이터 저장 경로 설정
        path = Application.dataPath + "/SaveFolder/";
        StartCoroutine(GoogleSheetManager.InitData());
        hasSavefile = new bool[4];
        for (int i = 0; i < hasSavefile.Length; i++)
        {
            // 세이브 데이터 파일이 존재할 경우 true
            if (File.Exists(path + $"{i}") == true)
            {
                hasSavefile[i] = true;
            }
        }
    } // InitManager

    //! 플레이어 데이터 세이브하는 함수
    public void SaveData()
    {
        string saveData = null;
        saveData = SaveInventoryData();
        saveData += SaveEquipSlotData();
        saveData += SaveBonfireList();
        Debug.Log(saveData);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "SaveData" + slotNum.ToString() + ".json", saveData);
    } // SaveData

    //! 인벤토리 데이터 저장하는 함수
    private string SaveInventoryData()
    {
        string data = null;
        string saveData = null;
        foreach (ItemData item in Inventory.Instance.inventory)
        {
            // 인벤토리안의 아이템들을 Json파일로 저장 아이템 구분: \n 사용
            data += JsonUtility.ToJson(item) + "\n";
        }
        // 저장된 Json파일의 데이터를 \n 기준으로 잘라서 배열에 저장
        string[] datas = data.Split('\n');
        Debug.Log(datas.Length);
        foreach (string _data in datas)
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

    //! 장비 슬롯의 데이터 저장하는 함수
    private string SaveEquipSlotData()
    {
        // 무기슬롯의 장착된 아이템 정보 저장 wSlot으로 구분
        string SaveEquipSlotData = wSlot + "\n";
        List<WeaponSlot> wSlotList = Inventory.Instance.weaponSlotList;
        for (int i = 0; i < wSlotList.Count; i++)
        {
            // 무기슬롯의 아이템이 있을 경우
            if (wSlotList[i].Item != null)
            {
                // JsonUtility은 딕셔너리를 지원하지않아 "번"을 기준으로 키값과 벨류값을 구분해서 저장함
                SaveEquipSlotData += $"{i}" + "번";
                SaveEquipSlotData += JsonUtility.ToJson(wSlotList[i].Item) + "\n";
            }
        }

        // 방어구슬롯의 장착된 아이템 정보 저장 aSlot으로 구분
        SaveEquipSlotData += aSlot + "\n";
        List<ArmorSlot> aSlotList = Inventory.Instance.armorSlotList;
        for (int i = 0; i < aSlotList.Count; i++)
        {
            // 방어구슬롯의 아이템이 있을 경우
            if (aSlotList[i].Item != null)
            {
                SaveEquipSlotData += $"{i}" + "번";
                SaveEquipSlotData += JsonUtility.ToJson(aSlotList[i].Item) + "\n";
            }
        }

        // 소모품슬롯의 장착된 아이템 정보 저장 cSlot으로 구분
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

        // 퀵슬롯 데이터 저장 qSlot으로 구분
        SaveEquipSlotData += qSlot + "\n";
        SaveEquipSlotData += UiManager.Instance.quickSlotBar.leftArmNum.ToString() + "/" +
            UiManager.Instance.quickSlotBar.rightArmNum.ToString() + "/" +
            UiManager.Instance.quickSlotBar.attackC_Num.ToString() + "/" +
            UiManager.Instance.quickSlotBar.recoveryC_Num.ToString() + "\n";
        return SaveEquipSlotData;
    } // SaveEquipSlotData

    //! 화톳불 리스트 저장하는 함수
    private string SaveBonfireList()
    {
        string bonfireData = bonfire + "\n";
        foreach (BonfireData bonfire in UiManager.Instance.warp.bonfireList)
        {
            bonfireData += JsonUtility.ToJson(bonfire) + "\n";
        }
        return bonfireData;
    } // SaveBonfireList

    //! 세이브데이터 로드하는 함수
    public void LoadData()
    {
        // 데이터 로드전 인벤토리 초기화
        for (int i = 0; i < Inventory.Instance.inventory.Count; i++)
        {
            Inventory.Instance.inventory[i] = null;
        }
        List<string> newItemDatas = new List<string>();
        // 저장된 Json파일을 불러옴
        string data = File.ReadAllText(path + "SaveData" + slotNum.ToString() + ".json");
        TextAsset inventoryData = new TextAsset(data);
        // \n을 기준으로 잘라서 배열에 데이터 저장
        string[] itemDatas = inventoryData.text.Split("\n");
        int number = 0;
        //Debug.Log($"불러온 세이브데이터의 길이 : {itemDatas.Length}");

        // 통합 인벤토리 데이터 로드
        for (int i = 0; i < itemDatas.Length - 1; i++)
        {
            // wSlot 값을 만나면 그 다음 줄부턴 무기슬롯 데이터
            if (itemDatas[i] == wSlot)
            {
                //Debug.Log("무기슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            // 통합 인벤토리의 데이터 파싱
            ItemData item = JsonUtility.FromJson<ItemData>(itemDatas[i]);
            //Debug.Log($"{i}번째 아이템 : {item.itemName}");
            Inventory.Instance.AddItem(item);
        }

        // 무기 장착 슬롯 데이터 로드
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // aSlot 값을 만나면 그 다음 줄부턴 방어구슬롯 데이터
            if (itemDatas[i] == aSlot)
            {
                //Debug.Log("방어구슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            // JsonUtility은 딕셔너리를 지원하지않아 "번"을 기준으로 키값과 벨류값을 구분해서 파싱함
            string[] wSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{wSlotData.Length} 인덱스 : {wSlotData[0]} 아이템 : {wSlotData[1]}");
            int wSlotIndex = int.Parse(wSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(wSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // 로드한 통합 인벤토리에서 장착중인 같은 아이템을 찾아서 무기슬롯에 등록
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.weaponSlotList[wSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // 방어구 장착 슬롯 데이터 로드
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // cSlot의 값을 만나면 그 다음 줄부턴 소모품슬롯 데이터
            if (itemDatas[i] == cSlot)
            {
                //Debug.Log("소모품슬롯 데이터 시작");
                number = i + 1;
                break;
            }
            string[] aSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{aSlotData.Length} 인덱스 : {aSlotData[0]} 아이템 : {aSlotData[1]}");
            int aSlotIndex = int.Parse(aSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(aSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // 로드한 통합 인벤토리에서 장착중인 같은 아이템을 찾아서 방어구슬롯에 등록
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.armorSlotList[aSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // 소모품 장착 슬롯 데이터 로드
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // qSlot의 값을 만나면 그 다음 줄부턴 퀵슬롯 데이터
            if (itemDatas[i] == qSlot)
            {
                number = i + 1;
                break;
            }
            string[] cSlotData = itemDatas[i].Split("번");
            //Debug.Log($"{cSlotData.Length} 인덱스 : {cSlotData[0]} 아이템 : {cSlotData[1]}");
            int cSlotIndex = int.Parse(cSlotData[0]);
            ItemData item = JsonUtility.FromJson<ItemData>(cSlotData[1]);
            foreach (ItemData _item in Inventory.Instance.inventory)
            {
                // 로드한 통합 인벤토리에서 장착중인 같은 아이템을 찾아서 소모품슬롯에 등록
                if (_item.itemID == item.itemID && _item.IsEquip == true)
                {
                    //Debug.Log($"인벤의 아이템과 동일한 아이템 발견 {_item.itemID} / {item.itemID}");
                    Inventory.Instance.consumptionSlotList[cSlotIndex].AddItem(_item);
                    break;
                }
            }
        }

        // 퀵슬롯 데이터 로드
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            // bonfire의 값을 만나면 그 다음 줄부턴 화톳불 데이터
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

        //! 화톳불 리스트 데이터 로드
        for (int i = number; i < itemDatas.Length - 1; i++)
        {
            BonfireData bonfire = JsonUtility.FromJson<BonfireData>(itemDatas[i]);
            // bonfireList에 불러온 화톳불데이터의 이름과 동일한 이름이 존재하지 않는 경우에만 add 및 slot생성
            if (!UiManager.Instance.warp.bonfireList.Any(b => b.bonfireName == bonfire.bonfireName))
            {
                UiManager.Instance.warp.bonfireList.Add(bonfire);
                UiManager.Instance.warp.CreateWarpSlot(bonfire);
            }
        }
        Debug.Log("저장된 데이터 로드 완료!");
    } // LoadData
} // DataManager