using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1fc7zvkSFdMGstxoSrExcC7ZaMutmBfqONeRNgoNVqW8/export?format=csv&range=A2:G";
    public List<ItemData> items = new List<ItemData>(); // ������ ���
    //! ���۽�Ʈ�� ��� ������ URL�� �������� �Լ�
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
