using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rampage : BossBase
{
    public Boss_Rampage_Status BossStatus { get; private set; }

    public GameObject rockPrefab;

    protected override void Init()
    {
        base.Init();
        SetState(new Boss_Idle_State(this));
    }

    public override IState Thought()
    {
        Debug.Log($"디버그");
        return new Boss_Rampage_RockRaise_State(this);
        // switch (BossStatus.currentPhase)
        // {
        //     case 1:
        //         Phase_1_Thought();
        //         break;
        //     case 2:
        //         Phase_2_Thought();
        //         break;
        // }
        // return null;
    }

    public IState Phase_1_Thought()
    {
        float randNum = Random.value;
        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsRangedChecked(Status.attackRange))
        {
            return new Boss_Idle_State(this);
        }
        else    //  플레이어가 근접공격 범위 밖에 있을 때
        {
            //  조건에 따라 원거리 공격 혹은 플레이어 추적
            if (randNum <= 0.6f)    //  40% 확률로 플레이어 추적
            {
                return new Boss_Chase_State(this);
            }
            else if (randNum <= 0.8f)   //  20% 확률로 원거리 공격
            {
                return new Boss_Rampage_RockRaise_State(this);
            }
            else if (randNum <= 0.9f)   //  10% 확률로 점프 공격
            {
                return new Boss_Chase_State(this);
            }
            else                        //  10% 확률로 돌진 공격
            {
                return new Boss_Chase_State(this);
            }

        }
    }

    public IState Phase_2_Thought()
    {
        return null;
    }

    #region Pattern
    public void RockRaise()
    {

    }

    public void RockThrow()
    {

    }
    #endregion
}
