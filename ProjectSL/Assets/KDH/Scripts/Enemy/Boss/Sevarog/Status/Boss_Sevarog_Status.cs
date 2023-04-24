using System;
using UnityEngine;

[Serializable]
public class Boss_Sevarog_Status : BossStatus
{
    /// <summary>
    /// 패턴 사용 횟수
    /// </summary>
    public int patternCount = default;

    /// <summary>
    /// 피격 횟수
    /// </summary>
    public int hitCount = default;


    /// <summary>
    /// 전멸기 사용 횟수
    /// </summary>
    public int enrageCount = default;
}