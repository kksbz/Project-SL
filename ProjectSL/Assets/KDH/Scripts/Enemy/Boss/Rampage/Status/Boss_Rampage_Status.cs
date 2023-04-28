using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Boss_Rampage_Status : BossStatus
{
    public LayerMask bossLayerMask;
    public LayerMask targetMask;
    public int bossLayerMaskIndex;
    public int targetLayerMaskIndex;
    public float groundSmashDistance;
    public float groundSmashMaxHeight = 10f;
    public float bodyTackleDistance;
    public float dodgeSpeed;
    public float dodgeMaxHeight = 5f;

    public float bodyTackleSpeed = 10f;



    #region Pattern Percentage
    public float attack_A_Percentage = default;
    public float attack_B_Percentage = default;
    public float attack_C_Percentage = default;
    public float dodge_Percentage = 0.6f;
    public float bodyTackle_Percentage = 0.3f;
    public float groundSmash_Percentage = 0.4f;
    public float rockThrow_Percentage = 0.6f;
    public float chase_Percentage = 1.0f;
    #endregion
}
