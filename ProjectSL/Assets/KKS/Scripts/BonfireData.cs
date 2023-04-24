using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BonfireData
{
    public bool hasBonfire = false; // 화톳불 활성화 유무
    public string activeSceneName; //화톳불이 존재하는 씬이름
    public string bonfireName; // 화톳불 이름
    public Vector3 bonfirePos; // 화톳불 위치

    public BonfireData(bool _hasBonfire, string sceneName, string name, Vector3 pos)
    {
        hasBonfire = _hasBonfire;
        activeSceneName = sceneName;
        bonfireName = name;
        bonfirePos = pos;
    } // BonfireData
} // BonfireData
