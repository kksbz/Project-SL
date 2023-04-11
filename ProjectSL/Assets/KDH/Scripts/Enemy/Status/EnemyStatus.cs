using System;
using UnityEngine;
[Serializable]
public class EnemyStatus : StatusBase
{
    public float detectionRange = default;
    public float attackRange = default;
    public LayerMask targetLayerMask = default;
    public LayerMask obstacleLayerMask = default;
}