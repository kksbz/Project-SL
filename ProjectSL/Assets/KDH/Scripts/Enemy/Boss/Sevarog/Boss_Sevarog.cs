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

    public List<GameObject> enragePillars = default;

    public List<GameObject> spawnEnemyPrefabs;

    // public Transform hammer;
    public Vector3 middlePos = new Vector3(-176f, 2f, 42f);

    protected override void Init()
    {
        base.Init();
        //SetState(new Boss_None_State(this));
        SetState(new Boss_Idle_State(this));
    }

    public override void TakeDamage(GameObject damageCauser, float damage)
    {
        if (Status.currentHp <= 0) return;

        if (Status.currentHp - damage <= 0)
        {
            Status.currentHp = 0;
            SetState(new Enemy_Die_State(this));
        }
        else
        {
            Status.currentHp -= damage;
            BossStatus.hitCount++;
        }
    }

    public override IState Thought()
    {
        float randNum_ = Random.value;

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
            case Boss_Sevarog_Swing2_1_Attack_State:
                if (randNum_ <= BossStatus.swing3_Percentage)
                {
                    //  뒤로 텔포
                    Debug.Log($"뒤로 텔포");
                    BossStatus.backTeleport = true;
                    return new Boss_Sevarog_Teleport_State(this);
                }
                break;
            case Boss_Sevarog_Swing1_Return_State:
            case Boss_Sevarog_Swing3_Attack_State:
                if (randNum_ <= BossStatus.teleport_Percentage)
                {
                    return new Boss_Sevarog_Teleport_State(this);
                }
                break;
            case Boss_Sevarog_Teleport_State:
                if (BossStatus.enrageCount == 1)
                {
                    BossStatus.enrageCount++;
                    return new Boss_Sevarog_Enrage_State(this);
                }
                if (BossStatus.backTeleport)
                {
                    return new Boss_Sevarog_Swing3_Attack_State(this);
                }
                return new Boss_Sevarog_Subjugation_State(this);
        }

        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsRangedChecked(Status.attackRange))
        {
            Debug.Log($"플레이어가 근접공격 범위 내에 있음");
            if (randNum_ <= BossStatus.swing1_Percentage)
            {
                return new Boss_Sevarog_Swing1_Attack_State(this);
            }
            else if (randNum_ <= BossStatus.swing2_Percentage)
            {
                return new Boss_Sevarog_Swing2_Attack_State(this);
            }
            else
            {
                return new Boss_Sevarog_Swing3_Attack_State(this);
            }

        }
        else    //  플레이어가 근접공격 범위 밖에 있을 때
        {
            // 확률에 따라 원거리 공격 혹은 플레이어 추적
            Debug.Log($"플레이어가 근접공격 범위 밖에 있음");
            if (randNum_ <= BossStatus.subjugation_Percentage)
            {
                return new Boss_Sevarog_Subjugation_State(this);
            }
            else if (randNum_ <= BossStatus.enemySpawn_Percentage)
            {
                return new Boss_Sevarog_EnemySpawn_State(this);
            }
            else
            {
                return new Boss_Chase_State(this);
            }
        }
    }

    public IState PreviousStateThought(float randNum)
    {
        switch (PreviousState)
        {
            case Boss_Sevarog_Swing2_Attack_State:
                if (randNum <= BossStatus.swing3_Percentage)
                {
                    //  뒤로 텔포
                    BossStatus.backTeleport = true;
                    return new Boss_Sevarog_Teleport_State(this);
                }
                else
                {
                    /*  Do Nothing  */
                }
                break;
            case Boss_Sevarog_Teleport_State:
                if (BossStatus.enrageCount == 1)
                {
                    BossStatus.enrageCount++;
                    return new Boss_Sevarog_Enrage_State(this);
                }
                return new Boss_Sevarog_Subjugation_State(this);
        }

        return null;
    }

    #region Pattern
    public void Teleport()
    {
        if (BossStatus.backTeleport)
        {
            //  타겟에 뒤로 텔레포트
            BossStatus.backTeleport = false;
            Vector3 targetBackPos_ = Target.TransformPoint(-Target.forward * 3f);
            Debug.Log($"뒤로 텔레포트 : {targetBackPos_}");
            Warp(targetBackPos_);

            TargetLook();

            return;
        }

        if (BossStatus.enrageCount == 1)
        {
            Warp(middlePos);
            TargetLook();
        }
        else
        {
            BossStatus.hitCount = 0;
            Warp();
            TargetLook();
        }
    }


    public void EnemySpawn()
    {
        float distance_ = 1.0f;

        Vector3 objectPos = transform.position;
        Vector3 objectRight = transform.right;

        Vector3 leftOffset = -distance_ * objectRight;
        Vector3 rightOffset = distance_ * objectRight;
        Vector3 leftCoord = transform.TransformPoint(leftOffset);
        Vector3 rightCoord = transform.TransformPoint(rightOffset);

        Instantiate(spawnEnemyPrefabs[0], leftCoord, Quaternion.identity);
        Instantiate(spawnEnemyPrefabs[1], rightCoord, Quaternion.identity);
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
