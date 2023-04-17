using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataParser
{
    //! ���۽�Ʈ�� �ۼ��� ItemDB�� item�� ��� List�� ��ȯ�ϴ� �Լ� 
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
