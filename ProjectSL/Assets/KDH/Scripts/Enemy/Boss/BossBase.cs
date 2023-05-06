using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBase : EnemyBase
{

    public bool IsPlayerJoined { get; protected set; }
    public bool IsIntroPlay { get; protected set; }
    protected override void Init()
    {
        base.Init();
    }

    public override void TakeDamage(GameObject damageCauser, float damage)
    {
        if (Status.currentHp <= 0) return;

        if (Status.currentHp - damage <= 0)
        {
            Status.currentHp = 0;
            SetState(new Boss_Die_State(this));
        }
        else
        {
            Status.currentHp -= damage;
        }
        UpdateHpBar(Status.currentHp);
    }

    public void OnPlayerJoin()
    {
        IsPlayerJoined = true;
    }

    public void OnIntroPlay()
    {
        IsIntroPlay = true;
    }

    public void OnComplete()
    {
    }

}