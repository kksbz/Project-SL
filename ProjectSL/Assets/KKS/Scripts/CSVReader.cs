using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    public static List<ItemData> Read(string fileName)
    {
        List<ItemData> itemDatas = new List<ItemData>();
        TextAsset csvData = new TextAsset(fileName);
        string[] data = csvData.text.Split("#LE");
        int a = 1;
        Debug.Log($"row data: {csvData.text}");
        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            Debug.Log($"{i}: {data[i]}");
            //string[] row = SplitCSVRow(data[i]);
            //if(row.Equals("#LE")) { continue; }
            //foreach(var r in row)
            //{
            //    Debug.Log($"{a} {row}");
            //    a++;
            //}
            //ItemData itemData = new ItemData(row);
            //itemDatas.Add(itemData);
        }
        return itemDatas;
    }

    private static string[] SplitCSVRow(string row)
    {
        List<string> rowValues = new List<string>();
        bool inQuotes = false;
        int startIndex = 0;

        for (int i = 0; i < row.Length; i++)
        {
            if (row[i] == '\"')
            {
                inQuotes = !inQuotes;
            }
            else if (row[i] == ',' && !inQuotes)
            {
                rowValues.Add(row.Substring(startIndex, i - startIndex));
                startIndex = i + 1;
            }
        }

        rowValues.Add(row.Substring(startIndex));

        return rowValues.ToArray();
    }
} // CSVReader
