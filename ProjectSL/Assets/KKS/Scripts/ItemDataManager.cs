using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager
{
    public List<ItemData> items = new List<ItemData>(); // 아이템 목록
    //! 구글시트에 작성된 ItemDB를 item에 담아 List로 저장하는 함수 
    public void LoadItems(string fileName)
    {
        //List<string[]> itemData = CSVReader.Read(fileName);
        //foreach (string[] data in itemData)
        //{
        //    ItemData item = new ItemData(data);
        //    items.Add(item);
        //}
    } // LoadItems
} // ItemDataManager
