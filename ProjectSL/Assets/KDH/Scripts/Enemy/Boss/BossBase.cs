using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBase : EnemyBase
{
    public Transform target;
    public bool IsPlayerJoined { get; protected set; }
    public bool IsIntroPlay { get; protected set; }
    protected override void Init()
    {
        base.Init();
        transform.GetChild(0).transform.rotation = Quaternion.identity;
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