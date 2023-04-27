using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rampage : BossBase
{
    [SerializeField]
    private Boss_Rampage_Status _bossStatus;
    public Boss_Rampage_Status BossStatus { get { return _bossStatus; } private set { _bossStatus = value; } }

    public Transform rockRaiseTransform;
    public GameObject rockPrefab;
    public GameObject rock;

    private Rigidbody _rb;

    protected override void Init()
    {
        base.Init();
        SetState(new Boss_Idle_State(this));

        _rb = GetComponent<Rigidbody>();
    }

    public override IState Thought()
    {
        switch (BossStatus.currentPhase)
        {
            case 1:
                return Phase_1_Thought();
            case 2:
                return Phase_2_Thought();
        }
        return null;
    }

    public IState Phase_1_Thought()
    {
        float randNum = Random.value;

        switch (PreviousState)
        {
            case Boss_Rampage_Attack_A_State:
            case Boss_Rampage_Attack_B_State:
            case Boss_Rampage_Attack_C_State:
                return new Boss_Rampage_Dodge_Start_State(this);
        }

        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsRangedChecked(Status.attackRange))
        {
            if (randNum <= 0.3f)
            {
                return new Boss_Rampage_Attack_A_State(this);
            }
            else if (randNum <= 0.6f)
            {
                return new Boss_Rampage_Attack_B_State(this);
            }
            else if (randNum <= 0.9f)
            {
                return new Boss_Rampage_Attack_C_State(this);
            }
            else
            {
                return new Boss_Rampage_Dodge_Start_State(this);
            }
        }
        else    //  플레이어가 근접공격 범위 밖에 있을 때
        {
            //  조건에 따라 원거리 공격 혹은 플레이어 추적
            if (randNum <= 0.6f)    //  60% 확률로 플레이어 추적
            {
                return new Boss_Chase_State(this);
            }
            else if (randNum <= 0.8f)   //  20% 확률로 원거리 공격
            {
                return new Boss_Chase_State(this);
            }
            else if (randNum <= 0.9f && IsRangedChecked(BossStatus.jumpAttackDistance))   //  10% 확률로 점프 공격
            {
                return new Boss_Chase_State(this);
            }
            else if (IsRangedChecked(BossStatus.bodyTackleDistance))    //  10% 확률로 돌진 공격
            {
                return new Boss_Chase_State(this);
            }
            else
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
        rock.SetActive(true);
    }

    public void RockThrow()
    {
        GameObject tempObject_ = Instantiate(rockPrefab, rock.transform.position, Quaternion.identity);
        tempObject_.GetComponent<Rock>().damage = 10f;
        tempObject_.GetComponent<Rock>().target = base.Target;
        tempObject_.GetComponent<Rock>().Throwing();
        rock.SetActive(false);
    }
    public void BodyTackle()
    {
        //  플레이어의 좌표 뒷 부분을 타겟으로 지점하고
        //  해당 부분으로 돌진을 시키면 될 듯
        //  콜라이더를 켜서 충돌 판정도 주면 될 듯

        //NavMeshAgent.SetDestination()
    }
    public void GroundSmash()
    {

    }
    public float initialVelocity = 5f; // adjust initial velocity as desired
    public float maxHeight = 5f; // adjust maximum height as desired
    public Vector3 targetPos;

    public void Dodge()
    {
        MoveController.NavMeshAgent.enabled = false;

        targetPos = transform.position - transform.forward * 8f;
        targetPos.y = 0f;
        Debug.Log(targetPos);

        Vector3 direction = targetPos - _rb.position;

        float distance = direction.magnitude;

        float horizontalVelocity = initialVelocity * (distance / Mathf.Sqrt(2 * maxHeight * Mathf.Abs(Physics.gravity.y)));

        float verticalVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * maxHeight);

        float timeToMaxHeight = verticalVelocity / Mathf.Abs(Physics.gravity.y);

        float totalTime = timeToMaxHeight + Mathf.Sqrt((2 * maxHeight) / Mathf.Abs(Physics.gravity.y));

        Vector3 velocity = horizontalVelocity * direction.normalized;

        velocity.y = verticalVelocity;

        _rb.velocity = velocity;
        transform.rotation = Quaternion.identity;

        // MoveController.NavMeshAgent.stoppingDistance = 0;
        // MoveController.SetSpeed(BossStatus.dodgeSpeed);
        // MoveController.NavMeshAgent.updateRotation = false;
        // MoveController.NavMeshAgent.SetDestination(targetPos);
    }
    public void DodgeComplete()
    {
        _rb.velocity = Vector3.zero;
        MoveController.NavMeshAgent.enabled = true;
        //SetFloat("ActionSpeed", 1f);
        // MoveController.SetSpeed(Status.currentMoveSpeed);
        // MoveController.NavMeshAgent.updateRotation = true;
        // MoveController.NavMeshAgent.stoppingDistance = 5;
    }
    public bool DodgeCompleteCheck()
    {
        float distance_ = Vector3.Distance(transform.position, targetPos);
        Debug.Log($"회피 남은 거리 : {distance_}");
        if (distance_ <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

}
