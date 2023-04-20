using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    //! CSV파일의 데이터를 파싱하는 함수
    public static List<string[]> CSVRead(string fileName)
    {
        // ','로 나눠진 CSV데이터를 담을 변수 초기화
        List<string[]> dataRows = new List<string[]>();
        // csv파일을 TextAtsset으로 형변환
        TextAsset csvData = new TextAsset(fileName);
        // csv파일에 데이터를 #LE를 기준으로 나눠서 배열에 저장 (#LE가 열의 마지막 값임)
        string[] data = csvData.text.Split("#LE");
        // #마지막줄 공백을 제외하기 위해 data.Length - 1 만큼 for문 시작
        for (int i = 0; i < data.Length - 1; i++)
        {
            // data의 값을 ',' 기준으로 잘라서 배열에 저장
            string[] rows = data[i].Split(new char[] { ',' });
            bool isNullRows = true;
            foreach (string row in rows)
            {
                // 데이터가 비어있는 곳은 제외
                if (!string.IsNullOrWhiteSpace(row))
                {
                    //Debug.Log($"데이터 있는 열 : {data[i]}");
                    isNullRows = false;
                    break;
                }
            }
            if (isNullRows == false)
            {
                //// List에 저장
                dataRows.Add(rows);
            }
        }
        return dataRows;
    } // CSVRead
} // CSVReader
