using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<string[]> itemDatas = new List<string[]>(); // ������ ������ ����Ʈ

    public override void InitManager()
    {
        StartCoroutine(GoogleSheetManager.InitData());
    } // InitManager
} // DataManager
