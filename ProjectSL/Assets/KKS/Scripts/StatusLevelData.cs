using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusLevelData
{
    public int hp; // 생명력
    public int mp; // 집중력
    public int st; // 지구력
    public int defense; // 체력
    public int damage; // 근력
    public float damageMultiplier; // 기량

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
