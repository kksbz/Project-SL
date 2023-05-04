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

    public Vector3 targetPos;

    protected override void Init()
    {
        base.Init();
        SetState(new Boss_Idle_State(this));

        _rb = GetComponent<Rigidbody>();

        BossStatus.bossLayerMaskIndex = LayerMask.NameToLayer("Enemy");
        BossStatus.targetLayerMaskIndex = LayerMask.NameToLayer("Target");
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
                if (randNum <= BossStatus.bodyTackle_Percentage)
                {
                    return new Boss_Ramapage_BodyTackle_State(this);
                }
                else
                {
                    return new Boss_Rampage_Dodge_Start_State(this);
                }
        }

        // 플레이어가 근접공격 범위 안에 있을 때
        if (IsInRange(Status.attackRange))
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
            if (randNum <= BossStatus.bodyTackle_Percentage)        // 일정 확률로 돌진 상태
            {
                return new Boss_Ramapage_BodyTackle_State(this);
            }
            else if (randNum <= BossStatus.groundSmash_Percentage)  // 일정 확률로 점프 공격
            {
                return new Boss_Rampage_GroundSmash_Start_State(this);
            }
            else if (randNum <= BossStatus.rockThrow_Percentage)    // 일정 확률로 원거리 공격
            {
                return new Boss_Ramapage_BodyTackle_State(this);
                //return new Boss_Rampage_RockRaise_State(this);
            }
            else                                                    // 일정 확률로 플레이어 추적
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

    public void GroundSmash()
    {

    }

    public void BodyTackle()
    {
        RigidbodyConstraints freezePosition = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        Debug.Log($"freezePosition {freezePosition}");
        RigidbodyMoveStart(freezePosition);

        targetPos = Target.position;
        targetPos.y = 0f;

        Vector3 direction = targetPos - _rb.position;

        Vector3 velocity = direction.normalized * BossStatus.bodyTackleSpeed;

        _rb.velocity = velocity;

        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
    }

    public void BodyTackleComplete()
    {
        RigidbodyMoveComplete();
    }

    public float initialVelocity = 5f;

    public void Jump(Vector3 newTargetPos, float maxHeight)
    {
        RigidbodyConstraints freezePosition = RigidbodyConstraints.FreezeRotation;

        RigidbodyMoveStart(freezePosition);

        targetPos = newTargetPos;

        Vector3 direction = targetPos - _rb.position;

        float distance = direction.magnitude;

        float horizontalVelocity = initialVelocity * (distance / Mathf.Sqrt(2 * maxHeight * Mathf.Abs(Physics.gravity.y)));

        float verticalVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * maxHeight);

        float timeToMaxHeight = verticalVelocity / Mathf.Abs(Physics.gravity.y);

        float totalTime = timeToMaxHeight + Mathf.Sqrt((2 * maxHeight) / Mathf.Abs(Physics.gravity.y));

        Vector3 velocity = horizontalVelocity * direction.normalized;

        velocity.y = verticalVelocity;

        _rb.velocity = velocity;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    public void JumpComplete()
    {
        RigidbodyMoveComplete();
    }

    public void RigidbodyMoveStart()
    {
        MoveController.NavMeshAgent.enabled = false;
        Physics.IgnoreLayerCollision(BossStatus.bossLayerMaskIndex, BossStatus.targetLayerMaskIndex, true);
    }
    public void RigidbodyMoveStart(RigidbodyConstraints freeze)
    {
        _rb.constraints = freeze;
        MoveController.NavMeshAgent.enabled = false;

        Physics.IgnoreLayerCollision(BossStatus.bossLayerMaskIndex, BossStatus.targetLayerMaskIndex, true);
    }

    public void RigidbodyMoveComplete()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        Physics.IgnoreLayerCollision(BossStatus.bossLayerMaskIndex, BossStatus.targetLayerMaskIndex, false);
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        _rb.velocity = Vector3.zero;
        targetPos = Vector3.zero;
        MoveController.NavMeshAgent.enabled = true;
    }

    public bool MoveCompleteCheck(float newDistance)
    {
        float distance_ = Vector3.Distance(transform.position, targetPos);
        Debug.Log($"distance : {distance_} / {targetPos}");
        if (distance_ <= newDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JumpCompleteCheck(float newDistance)
    {
        Vector3 planePos = new Vector3(transform.position.x, 0f, transform.position.y);
        float distance_ = Vector3.Distance(transform.position, planePos);
        if (distance_ <= newDistance)
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
