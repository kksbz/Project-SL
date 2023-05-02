using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSL.Enemy;

public class Enemy_Knight : EnemyBase
{
    protected override void Init()
    {
        base.Init();
        SetState(new Enemy_Idle_State(this));
    }

    public override IState Thought()
    {
        //  이전 상태에 따른 상태 전환
        switch (PreviousState)
        {
            case Enemy_Attack_State:
                return new Enemy_Dodge_State(this);
        }

        //  플레이어를 찾지 못했거나 플레이어를 놓쳤다면
        if (!IsFieldOfViewFind() || IsMissed(Status.detectionRange))
        {
            return new Enemy_Patrol_State(this);

        }
        else
        {
            Debug.Log($"IsArrive : {IsArrive(Status.attackRange)}");
            //  플레이어가 공격범위 내에 있다면
            if (IsArrive(Status.attackRange))
            {
                return new Enemy_Attack_State(this);
            }
            return new Enemy_Chase_State(this);
        }

    }
    public override int RandomAttack()
    {
        float randNum_ = Random.value;

        if (randNum_ <= 0.33f)
        {
            return 1;
        }
        else if (randNum_ <= 0.66f)
        {
            return 2;
        }
        else
        {
            return 3;
        }

    }

    public override void Attack(string CurrentAnimationName, int onActionIndex)
    {
        switch (CurrentAnimationName)
        {
            case EnemyDefineData.ANIMATION_ATTACK_01:
                NotAttackColliderEnabled(0);
                break;
            case EnemyDefineData.ANIMATION_ATTACK_02:
                NotAttackColliderEnabled(1);
                break;
            case EnemyDefineData.ANIMATION_ATTACK_03:
                if (onActionIndex < 2)
                {
                    NotAttackColliderEnabled(0);
                }
                else
                {
                    NotAttackColliderEnabled(1);
                }
                break;
            case EnemyDefineData.ANIMATION_ATTACK_04:
                NotAttackColliderEnabled(0);
                break;
            case EnemyDefineData.ANIMATION_ATTACK_05:
                NotAttackColliderEnabled(0);
                NotAttackColliderEnabled(1);
                break;
        }
    }
}
