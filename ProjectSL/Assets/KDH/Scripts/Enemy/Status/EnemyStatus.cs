using System;
using UnityEngine;
[Serializable]
public class EnemyStatus : StatusBase
{
    public float detectionRange = default;
    public float attackRange = default;
    public float dodge_Percentage = default;
}

[Serializable]
public class EnemyResearchStatus
{
    public float viewRadius = default;
    [Range(0, 360)]
    public float viewAngle = default;
    public LayerMask targetLayerMask = default;
    public LayerMask obstacleLayerMask = default;
}