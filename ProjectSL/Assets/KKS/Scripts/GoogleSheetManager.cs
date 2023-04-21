using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager
{
    const string URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:J";
    //! 구글시트에 담긴 정보를 URL로 가져오는 함수
    public static IEnumerator InitData()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string itemDataBase = www.downloadHandler.text;
        // URL로 가져온 구글시트의 아이템데이터를 파싱함
        List<string[]> itemDatas = CSVReader.CSVRead(itemDataBase);
        // 데이터매니저 itemDatas 변수에 파싱된 아이템리스트를 캐싱
        DataManager.Instance.itemDatas = itemDatas;
        //Debug.Log(itemDataBase);
        Debug.Log("데이터 불러오기 완료");
    } // Start
} // GoogleSheetManager
