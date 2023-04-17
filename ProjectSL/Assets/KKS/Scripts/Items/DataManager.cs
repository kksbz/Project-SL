using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // 아이템 데이터 리스트

    public override void InitManager()
    {
        StartCoroutine(GoogleSheetManager.InitData());
    } // InitManager
} // DataManager
