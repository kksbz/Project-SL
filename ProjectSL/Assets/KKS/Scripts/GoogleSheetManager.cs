using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    // ������ ������ ���̺� �ּ�
    const string ITEMDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:P";
    // ����ġ ���̺� �ּ�
    const string EXPERIENCEDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:C&gid=1728483937";
    // ��� ���̺� �ּ�
    const string DROPTABLEDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:H&gid=1955775546";
    // �������ͽ� ���� ���̺� �ּ�
    const string STATUSLEVELDATA_URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:H&gid=1873222712";

    //! ���۽�Ʈ�� ��� ������������ URL�� �������� �Լ�
    public static IEnumerator InitItemData()
    {
        UnityWebRequest wwwItemDatas = UnityWebRequest.Get(ITEMDATA_URL);
        yield return wwwItemDatas.SendWebRequest();
        string itemDataBase = wwwItemDatas.downloadHandler.text;
        // URL�� ������ ���۽�Ʈ�� �����۵����͸� �Ľ���
        List<string[]> itemDatas = CSVReader.CSVRead(itemDataBase);
        // �����͸Ŵ��� itemDatas ������ �Ľ̵� �����۸���Ʈ�� ĳ��
        DataManager.Instance.itemDatas = itemDatas;
        //Debug.Log(itemDataBase);
        Debug.Log("������ �ҷ����� �Ϸ�");
    } // Start

    //! ���۽�Ʈ�� ��� ����ġ���̺� ������ URL�� �������� �Լ�
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
        //    Debug.Log($"Ű�� : {data.Key}, ������ : {data.Value}");
        //}
    } // InitExperienceData

    //! ���۽�Ʈ�� ��� ������̺� ������ URL�� �������� �Լ�
    public static IEnumerator InitDropTableData()
    {
        UnityWebRequest wwwDropTableDatas = UnityWebRequest.Get(DROPTABLEDATA_URL);
        yield return wwwDropTableDatas.SendWebRequest();
        string dropTable = wwwDropTableDatas.downloadHandler.text;
        List<string[]> dropTableData = CSVReader.CSVRead(dropTable);

        Dictionary<string, List<string>> dropTableDic = new Dictionary<string, List<string>>();
        foreach (string[] dropData in dropTableData)
        {
            string key = dropData[0].Replace("\r\n", string.Empty);
            Debug.Log($"Ű�� {key}, Ű���� ���� {key.Length}");
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
        //        Debug.Log($"{data.Key} �� ����� : {data.Value[i]}");
        //    }
        //    Debug.Log("=======================================");
        //}
    } // InitDropTableData

    //! ���۽�Ʈ�� ��� �������ͽ� ���� ���̺� ������ URL�� �������� �Լ�
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
            //Debug.Log($"���ݷ������̺� Ű�� : {key}, ������: {value.damageMultiplier}");
        }
        DataManager.Instance.statusLevelData = statusLevelData;
    } // InitStatusPerLevelData
} // GoogleSheetManager
