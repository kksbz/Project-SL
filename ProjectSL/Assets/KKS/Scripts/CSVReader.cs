using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    //! CSV������ �����͸� �Ľ��ϴ� �Լ�
    public static List<string[]> CSVRead(string fileName)
    {
        // ','�� ������ CSV�����͸� ���� ���� �ʱ�ȭ
        List<string[]> dataRows = new List<string[]>();
        // csv������ TextAtsset���� ����ȯ
        TextAsset csvData = new TextAsset(fileName);
        // csv���Ͽ� �����͸� #LE�� �������� ������ �迭�� ���� (#LE�� ���� ������ ����)
        string[] data = csvData.text.Split("#LE");

        // #LE���� �����ϱ� ���� data.Length - 1 ��ŭ for�� ����
        for (int i = 0; i < data.Length - 1; i++)
        {
            // data�� ���� ',' �������� �߶� �迭�� ����
            string[] rows = data[i].Split(new char[] { ',' });
            // List�� ����
            dataRows.Add(rows);
        }
        return dataRows;
    } // CSVRead
} // CSVReader
