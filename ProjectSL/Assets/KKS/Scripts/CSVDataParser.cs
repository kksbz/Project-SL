using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataParser
{
    //! 구글시트에 작성된 ItemDB를 item에 담아 List로 반환하는 함수 
    public static List<ItemData> ItemDataParser(List<string[]> _dataRows)
    {
        List<ItemData> itemDatas = new List<ItemData>();
        for (int i = 0; i < _dataRows.Count; i++)
        {
            ItemData item = new ItemData(_dataRows[i]);
            itemDatas.Add(item);
        }
        return itemDatas;
    } // ItemDataParser
} // CSVDataParser
