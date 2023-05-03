using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusLevelData
{
    public int hp; // �����
    public int mp; // ���߷�
    public int st; // ������
    public int defense; // ü��
    public int damage; // �ٷ�
    public float damageMultiplier; // �ⷮ

    public StatusLevelData(string[] statusData)
    {
        hp = int.Parse(statusData[1]);
        mp = int.Parse(statusData[2]);
        st = int.Parse(statusData[3]);
        defense = int.Parse(statusData[4]);
        damage = int.Parse(statusData[5]);
        damageMultiplier = float.Parse(statusData[6]);
    } // StatusLevelData
} // StatusLevelData
