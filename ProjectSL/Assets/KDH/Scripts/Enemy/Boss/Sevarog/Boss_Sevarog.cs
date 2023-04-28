using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Sevarog : BossBase
{
    [SerializeField]
    private Boss_Sevarog_Status _bossStatus;
    public Boss_Sevarog_Status BossStatus { get { return _bossStatus; } private set { _bossStatus = value; } }

    [Header("Pattern Object")]
    [Tooltip("원거리 공격 Prefab")]
    public GameObject subjugationPrefab = default;

    [Tooltip("전멸기 Prefab")]
    public GameObject enragePrefab = default;

    [Tooltip("전멸기 기둥 Prefab")]
    public GameObject enragePillarPrefab = default;

    // public Transform hammer;
    public Vector3 middlePos = new Vector3(-176f, 2f, 42f);

    protected override void Init()
    {
        base.Init();
        //SetState(new Boss_None_State(this));
        SetState(new Boss_Idle_State(this));
    }

    public override void TakeDamage(float damage)
    {
        if (Status.currentHp - damage <= 0)
        {
            Status.currentHp = 0;
        }
        else
        {
            Status.currentHp -= damage;
            BossStatus.hitCount++;
        }
    }
    public override IState Thought(Transform newTarget)
    {
        return null;
    }

    public override IState Thought()
    {
        Debug.Log($"current Hp : {Status.currentHp} / enrageCount : {BossStatus.enrageCount} / hitCount : {BossStatus.hitCount}");
        if (Status.currentHp <= Status.maxHp * 0.5f && BossStatus.enrageCount < 1)
        {
            BossStatus.enrageCount++;
            return new Boss_Sevarog_Teleport_State(this);
        }

        //  피격 횟수가 5회 이상이면 텔레포트
        if (5 <= BossStatus.hitCount)
        {
            return new Boss_Sevarog_Teleport_State(this);
        }

        Debug.Log($"Previous State : {PreviousState.ToString()}");

        switch (PreviousState)
        {
            case Boss_Sevarog_Teleport_State:
                if (BossStatus.enrageCount == 1)
                {
                    BossStatus.enrageCount++;
                    return new Boss_Sevarog_Enrage_State(this);
                }
                return new Boss_Sevarog_Subjugation_State(this);
        }

        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsRangedChecked(Status.attackRange))
        {
            Debug.Log($"플레이어가 근접공격 범위 내에 있음 Swing1Attack State로 전환");
            return new Boss_Sevarog_Swing1Attack_State(this);
        }
        else    //  플레이어가 근접공격 범위 밖에 있을 때
        {
            //  조건에 따라 원거리 공격 혹은 플레이어 추적
            Debug.Log($"플레이어가 근접공격 범위 밖에 있음 Chase State로 전환");
            return new Boss_Chase_State(this);
        }
    }

    #region Pattern
    public void Teleport()
    {
        if (BossStatus.enrageCount == 1)
        {
            Warp(middlePos);
        }
        else
        {
            BossStatus.hitCount = 0;
            Warp();
        }
    }

    public void SubjugationPattern()
    {
        GameObject object_ = Instantiate(subjugationPrefab, Target.position, Quaternion.identity);
        object_.GetComponent<Subjugation>().damage = Status.currentAttackDamage;
    }

    public void EnragePattern()
    {
        enragePrefab.SetActive(true);
        enragePrefab.GetComponent<Enrage>().damage = Status.currentAttackDamage;

        List<GameObject> pillars_ = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                float x_ = -168f - 16 * i;
                float y_ = 50f - 16 * j;
                Vector3 pillarPos_ = new Vector3(x_, -2f, y_);
                Debug.Log($"pillarPos : {pillarPos_}");
                GameObject tempObject_ = Instantiate(enragePillarPrefab, pillarPos_, Quaternion.identity);
                pillars_.Add(tempObject_);
            }
        }
        enragePrefab.GetComponent<Enrage>().pillars = pillars_;
    }
    #endregion
}
