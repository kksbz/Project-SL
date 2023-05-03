using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    // 아이템 데이터 테이블 주소
    const string ITEMDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:P";
    // 경험치 테이블 주소
    const string EXPERIENCEDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:C&gid=1728483937";
    // 드랍 테이블 주소
    const string DROPTABLEDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:H&gid=1955775546";
    // 스테이터스 레벨 테이블 주소
    const string STATUSLEVELDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:H&gid=1873222712";

    //! 구글시트에 담긴 아이템정보를 URL로 가져오는 함수
    public static IEnumerator InitItemData()
    {
        UnityWebRequest wwwItemDatas = UnityWebRequest.Get(ITEMDATA_URL);
        yield return wwwItemDatas.SendWebRequest();
        string itemDataBase = wwwItemDatas.downloadHandler.text;
        // URL로 가져온 구글시트의 아이템데이터를 파싱함
        List<string[]> itemDatas = CSVReader.CSVRead(itemDataBase);
        // 데이터매니저 itemDatas 변수에 파싱된 아이템리스트를 캐싱
        DataManager.Instance.itemDatas = itemDatas;
        //Debug.Log(itemDataBase);
        Debug.Log("데이터 불러오기 완료");
    } // Start

    //! 구글시트에 담긴 경험치테이블 정보를 URL로 가져오는 함수
    public static IEnumerator InitExperienceData()
    {
        UnityWebRequest wwwExperienceDatas = UnityWebRequest.Get(EXPERIENCEDATA_URL);
        yield return wwwExperienceDatas.SendWebRequest();
        string experienceBase = wwwExperienceDatas.downloadHandler.text;
        List<string[]> experienceDatas = CSVReader.CSVRead(experienceBase);

        Dictionary<int, int> experienceDic = new Dictionary<int, int>();
        foreach (string[] experienceData in experienceDatas)
        {
            int key = int.Parse(experienceData[0]);
            int value = int.Parse(experienceData[1]);
            experienceDic.Add(key, value);
        }
        DataManager.Instance.experienceDatas = experienceDic;
        //foreach(var data in DataManager.Instance.experienceDatas)
        //{
        //    Debug.Log($"키값 : {data.Key}, 벨류값 : {data.Value}");
        //}
    } // InitExperienceData

    //! 구글시트에 담긴 드랍테이블 정보를 URL로 가져오는 함수
    public static IEnumerator InitDropTableData()
    {
        UnityWebRequest wwwDropTableDatas = UnityWebRequest.Get(DROPTABLEDATA_URL);
        yield return wwwDropTableDatas.SendWebRequest();
        string dropTable = wwwDropTableDatas.downloadHandler.text;
        List<string[]> dropTableData = CSVReader.CSVRead(dropTable);

        Dictionary<string, List<string>> dropTableDic = new Dictionary<string, List<string>>();
        foreach (string[] dropData in dropTableData)
        {
            string key = dropData[0];
            List<string> value = new List<string>();
            for (int i = 1; i < dropData.Length - 1; i++)
            {
                value.Add(dropData[i]);
            }
            dropTableDic.Add(key, value);
        }
        DataManager.Instance.dropTable = dropTableDic;
        //foreach (var data in DataManager.Instance.dropTable)
        //{
        //    for (int i = 0; i < data.Value.Count; i++)
        //    {
        //        Debug.Log($"{data.Key} 의 드랍템 : {data.Value[i]}");
        //    }
        //    Debug.Log("=======================================");
        //}
    } // InitDropTableData

    //! 구글시트에 담긴 스테이터스 레벨 테이블 정보를 URL로 가져오는 함수
    public static IEnumerator InitStatusPerLevelData()
    {
        UnityWebRequest wwwStatusDatas = UnityWebRequest.Get(STATUSLEVELDATA_URL);
        yield return wwwStatusDatas.SendWebRequest();
        string statusTable = wwwStatusDatas.downloadHandler.text;
        List<string[]> statusTableData = CSVReader.CSVRead(statusTable);

        Dictionary<int, StatusLevelData> statusLevelData = new Dictionary<int, StatusLevelData>();
        foreach (var data in statusTableData)
        {
            int key = int.Parse(data[0]);
            StatusLevelData value = new StatusLevelData(data);
            statusLevelData.Add(key, value);
            //Debug.Log($"스텟레벨테이블 키값 : {key}, 벨류값: {value.damageMultiplier}");
        }
        DataManager.Instance.statusLevelData = statusLevelData;
    } // InitStatusPerLevelData
} // GoogleSheetManager
