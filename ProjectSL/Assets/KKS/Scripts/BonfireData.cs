using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BonfireData
{
    public bool hasBonfire = false; // ȭ��� Ȱ��ȭ ����
    public string activeSceneName; //ȭ����� �����ϴ� ���̸�
    public string bonfireName; // ȭ��� �̸�
    public Vector3 bonfirePos; // ȭ��� ��ġ

    public BonfireData(bool _hasBonfire, string sceneName, string name, Vector3 pos)
    {
        hasBonfire = _hasBonfire;
        activeSceneName = sceneName;
        bonfireName = name;
        bonfirePos = pos;
    } // BonfireData
} // BonfireData
