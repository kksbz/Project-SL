using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:H";
    public List<ItemData> items = new List<ItemData>(); // 아이템 목록
    //! 구글시트에 담긴 정보를 URL로 가져오는 함수
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string itemDataBase = www.downloadHandler.text;

        // URL로 가져온 구글시트의 아이템데이터를 파싱함
        List<string[]> itemDatas = CSVReader.CSVRead(itemDataBase);
        List<ItemData> items = CSVDataParser.ItemDataParser(itemDatas);
        // 아이템매니저 items 변수에 파싱된 아이템리스트를 캐싱
        ItemManager.Instance.items = items;
    } // Start
} // GoogleSheetManager
