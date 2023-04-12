using System;
using UnityEngine;
[Serializable]
public class EnemyStatus : StatusBase
{
    public float detectionRange = default;
    public float attackRange = default;
}

[Serializable]
public class EnemyResearchStatus
{
    public float viewRadius = default;
    public float viewAngle = default;
    public LayerMask targetLayerMask = default;
    public LayerMask obstacleLayerMask = default;
}