using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    const string URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:P";
    //! ���۽�Ʈ�� ��� ������ URL�� �������� �Լ�
    public static IEnumerator InitData()
    {
        UnityWebRequest wwwItemDatas = UnityWebRequest.Get(URL);
        yield return wwwItemDatas.SendWebRequest();
        string itemDataBase = wwwItemDatas.downloadHandler.text;
        // URL�� ������ ���۽�Ʈ�� �����۵����͸� �Ľ���
        List<string[]> itemDatas = CSVReader.CSVRead(itemDataBase);
        // �����͸Ŵ��� itemDatas ������ �Ľ̵� �����۸���Ʈ�� ĳ��
        DataManager.Instance.itemDatas = itemDatas;
        //Debug.Log(itemDataBase);
        Debug.Log("������ �ҷ����� �Ϸ�");
    } // Start
} // GoogleSheetManager
