using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:G";
    public List<ItemData> items = new List<ItemData>(); // 아이템 목록
    //! 구글시트에 담긴 정보를 URL로 가져오는 함수
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string itemDataBase = www.downloadHandler.text;

        List<ItemData> itemDatas = CSVReader.Read(itemDataBase);
        //foreach (ItemData itemData in itemDatas)
        //{
        //    Debug.Log($"{itemData.itemID}, {itemData.itemName}, {itemData.itemType}, {itemData.description}");
        //}
    } // Start
} // GoogleSheetManager
