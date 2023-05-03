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

    public bool backTeleport;

    public float swing1_Percentage = 0.5f;
    public float swing2_Percentage = 0.75f;
    public float swing3_Percentage = 1f;
    public float teleport_swing3_Percentage = 0.5f;
    public float subjugation_Percentage = 0.2f;
    public float enemySpawn_Percentage = 0.4f;

    public float teleport_Percentage = 0.2f;
}