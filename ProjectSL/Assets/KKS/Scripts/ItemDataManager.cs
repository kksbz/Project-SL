using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager
{
    public List<ItemData> items = new List<ItemData>(); // ������ ���
    //! ���۽�Ʈ�� �ۼ��� ItemDB�� item�� ��� List�� �����ϴ� �Լ� 
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
