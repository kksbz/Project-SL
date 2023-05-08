using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSL.Enemy;

public class EnemyBase : CharacterBase, GData.IDamageable, GData.IGiveDamageable
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    [SerializeField]
    protected EnemyResearchStatus researchStatus = default;
    protected IStateMachine stateMachine = default;
    protected IEnemyMoveController moveController = default;
    protected IEnemyAnimator enemyAnimator = default;
    protected IEnemyTargetResearch targetResearch = default;
    protected IEnemyHpBar enemyHpBar = default;
    protected ISFX_Object enemy_SFX = default;

    [Tooltip("공격시 활성화 될 공격 범위 콜라이더")]
    [SerializeField]
    protected List<Collider> attackCollider = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public EnemyResearchStatus ResearchStatus { get { return researchStatus; } protected set { researchStatus = value; } }
    public IStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public IEnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public IEnemyAnimator EnemyAnimator { get { return enemyAnimator; } protected set { enemyAnimator = value; } }
    public IEnemyTargetResearch TargetResearch { get { return targetResearch; } protected set { targetResearch = value; } }
    public IEnemyHpBar EnemyHpBar { get { return enemyHpBar; } protected set { enemyHpBar = value; } }
    public ISFX_Object Enemy_SFX { get { return enemy_SFX; } protected set { enemy_SFX = value; } }
    #endregion

    public string currentState;

    protected void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        StateMachine = new EnemyStateMachine();

        TryGetComponent<IEnemyAnimator>(out enemyAnimator);
        TryGetComponent<IEnemyMoveController>(out moveController);
        TryGetComponent<IEnemyTargetResearch>(out targetResearch);
        TryGetComponent<IEnemyHpBar>(out enemyHpBar);

        EnemyAnimator.Init();

        MoveController.Init();
        SetSpeed(Status.currentMoveSpeed);

        TargetResearch.Init(ResearchStatus, new FieldOfView(transform, ResearchStatus));

        EnemyHpBar.Init();
        InitHpBar(Status.maxHp, Status.currentHp);

        if (TryGetComponent<ISFX_Object>(out enemy_SFX))
        {
            Enemy_SFX.Init();
        }

        //SetState(new Enemy_Idle_State(this));
    }

    protected void Update()
    {
        StateMachine.Update();
        currentState = CurrentState.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        GData.IDamageable object_ = other.GetComponent<GData.IDamageable>();

        //  { Enemy끼리 서로 데미지를 못 입히도록 예외 처리 추가
        EnemyBase enemy_ = other.GetComponent<EnemyBase>();

        if (object_ == null || object_ == default || enemy_ != null || enemy_ != default)
        {
            /*  Do Nothing  */
        }
        else
        {
            GiveDamage(object_, Status.currentAttackDamage);
        }
    }

    public virtual void GiveDamage(GData.IDamageable damageable, float damage)
    {
        Debug.Log($"데미지를 입힘 / 대상 : {damageable.ToString()} / 데미지 : {damage}");
        damageable.TakeDamage(gameObject, damage);
    }

    public virtual void TakeDamage(GameObject damageCauser, float damage)
    {
        if (Status.currentHp <= 0) return;

        ActiveHpBar();

        if (Status.currentHp - damage <= 0)
        {
            Status.currentHp = 0;
            SetState(new Enemy_Die_State(this));
        }
        else
        {
            Status.currentHp -= damage;
            SetState(new Enemy_Hit_State(this));
        }
        UpdateHpBar(Status.currentHp);
    }

    public virtual void OnDie()
    {
        DropReward();
        Destroy(gameObject);
    }

    public virtual void DropReward()
    {
        List<string> rewardList = DataManager.Instance.dropTable[Status.name];
        foreach (var iterator in rewardList)
        {
            Debug.Log($"DropReward Debug : {iterator}");
        }

        int itemIndex = default;

        int randNum_ = Random.Range(0, rewardList.Count);

        foreach (var iterator in DataManager.Instance.itemDatas)
        {
            if (rewardList[randNum_] == iterator[1])
            {
                itemIndex = int.Parse(iterator[0]);
                Debug.Log($"Item Debug : 아이템 이름 {rewardList[randNum_]} / 아이템 인덱스 : {iterator[0]}");
            }
        }

        GameObject item = Instantiate(Resources.Load<GameObject>($"KKS/Prefabs/Item/{itemIndex}"));
        item.transform.position = transform.position + (Vector3.up * 0.3f);

        UiManager.Instance.soulBag.GetSoul(int.Parse(rewardList[0]));
    }

    public virtual IState Thought()
    {
        return null;
    }

    public virtual void Attack(string CurrentAnimationName, int onActionIndex)
    {

    }
    public virtual int RandomAttack()
    {
        return 0;
    }
    public virtual void TargetLook()
    {
        Vector3 dir_ = Target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir_);
        transform.rotation = rotation;
    }
    public virtual void TargetLook(Transform newTarget)
    {
        Vector3 dir_ = newTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir_);
        transform.rotation = rotation;
    }

    #region EnemyHpBar
    public void InitHpBar(float maxHp, float currentHp)
    {
        EnemyHpBar.InitHpBar(maxHp, currentHp);
    }
    public void ActiveHpBar()
    {
        EnemyHpBar.ActiveHpBar();
    }
    public void UpdateHpBar(float newHp)
    {
        EnemyHpBar.UpdateHpBar(newHp);
    }
    #endregion

    #region AttackCollider
    public List<Collider> AttackCollider { get { return attackCollider; } protected set { attackCollider = value; } }
    public void SetAttackColliderEnabled(bool newEnabled)
    {
        foreach (var iterator in AttackCollider)
        {
            iterator.enabled = newEnabled;
        }
    }
    public void SetAttackColliderEnabled(int index, bool newEnabled)
    {
        AttackCollider[index].enabled = newEnabled;
    }
    public void NotAttackColliderEnabled()
    {
        foreach (var iterator in AttackCollider)
        {
            iterator.enabled = !iterator.enabled;
        }
    }
    public void NotAttackColliderEnabled(int index)
    {
        AttackCollider[index].enabled = !AttackCollider[index].enabled;
    }
    #endregion

    #region StateMachine
    public IState CurrentState { get { return StateMachine.CurrentState; } }
    public IState PreviousState { get { return StateMachine.PreviousState; } }
    public void SetState(IState newState)
    {
        StateMachine.SetState(newState);
    }
    public void OnAction()
    {
        StateMachine.OnAction();
    }
    #endregion

    #region IEnemyMoveController
    public List<Transform> PatrolPoints { get { return MoveController.PatrolPoints; } }
    public Transform Target { get { return MoveController.Target; } }
    //public UnityEngine.AI.NavMeshAgent NavMeshAgent { get { return MoveController.NavMeshAgent; } }
    public void SetStoppingDistance(float newDistance)
    {
        MoveController.SetStoppingDistance(newDistance);
        //NavMeshAgent.stoppingDistance = distance;
    }
    public void SetSpeed(float newSpeed)
    {
        //NavMeshAgent.speed = newSpeed;
        MoveController.SetSpeed(newSpeed);
    }
    public void SetStop(bool isStopped)
    {
        //NavMeshAgent.isStopped = isStopped;
        MoveController.SetStop(isStopped);
    }
    public void SetUpdateRotation(bool isRotation)
    {
        //NavMeshAgent.updateRotation = isRotation;
        MoveController.SetUpdateRotation(isRotation);
    }
    public void Patrol()
    {
        MoveController.Patrol();
    }
    public void PatrolStop()
    {
        MoveController.PatrolStop();
    }
    public void TargetFollow(Transform newTarget)
    {
        MoveController.TargetFollow(newTarget);
    }
    public void TargetFollow(Vector3 newPosition)
    {
        MoveController.TargetFollow(newPosition);
    }
    public void TargetFollow(Transform newTarget, bool isFollow)
    {
        MoveController.TargetFollow(newTarget, isFollow);
    }
    public void TargetFollow(Vector3 newPosition, bool isFollow)
    {
        MoveController.TargetFollow(newPosition, isFollow);
    }
    public void Warp()
    {
        MoveController.Warp();
    }
    public void Warp(Vector3 newPos)
    {
        MoveController.Warp(newPos);
    }
    public bool IsArrive(float distance)
    {
        return MoveController.IsArrive(distance);
    }
    public bool IsArrivePatrol(float distance)
    {
        return MoveController.IsArrivePatrol(distance);
    }
    public bool IsInRange(float distance)
    {
        return MoveController.IsInRange(distance);
    }
    public bool IsPositionReachable(Vector3 newPosition)
    {
        return MoveController.IsPositionValid(newPosition);
    }
    #endregion

    #region EnemyTargetResearch
    public List<Transform> ChaseTargets { get { return TargetResearch.Targets; } }
    public IEnumerator FieldOfViewSearch(float delay)
    {
        return TargetResearch.FieldOfViewSearch(delay);
    }
    public bool IsFieldOfViewFind()
    {
        return TargetResearch.IsFieldOfViewFind;
    }
    #endregion

    #region IEnemyAnimator
    public AnimatorStateInfo CurrentStateInfo { get { return EnemyAnimator.CurrentStateInfo; } }
    public bool IsName(string animationName)
    {
        return EnemyAnimator.CurrentStateInfo.IsName(animationName);
    }
    public void SetTrigger(string parameter)
    {
        EnemyAnimator.SetTrigger(parameter);
    }
    public void SetBool(string parameter, bool value)
    {
        EnemyAnimator.SetBool(parameter, value);
    }
    public void SetFloat(string parameter, float value)
    {
        EnemyAnimator.SetFloat(parameter, value);
    }
    public void SetInt(string parameter, int value)
    {
        EnemyAnimator.SetInt(parameter, value);
    }
    public bool IsAnimationEnd()
    {
        return EnemyAnimator.IsAnimationEnd();
    }
    public bool IsAnimationEnd(string animationName)
    {
        return EnemyAnimator.IsAnimationEnd(animationName);
    }
    public bool IsAnimationPlaying(string animationName)
    {
        return enemyAnimator.IsAnimationPlaying(animationName);
    }
    #endregion

    #region ISFX_Object
    public void SetAudioClip(AudioClip audioClip)
    {
        Enemy_SFX.SetAudioClip(audioClip);
    }

    public void SFX_Play()
    {
        Enemy_SFX.SFX_Play();
    }

    public void SFX_Play(AudioClip audioClip)
    {
        Enemy_SFX.SFX_Play(audioClip);
    }

    public void SFX_Play(AudioClip audioClip, bool isOneShot)
    {
        Enemy_SFX.SFX_Play(audioClip, isOneShot);
    }
    public void SFX_Play_Loop(AudioClip audioClip)
    {
        Enemy_SFX.SFX_Play_Loop(audioClip);
    }

    public void SFX_Stop()
    {
        Enemy_SFX.SFX_Stop();
    }

    public AudioClip FindAudioClip(string audioClipName)
    {
        AudioClip findClip_ = Enemy_SFX.FindAudioClip(audioClipName);
        Debug.Log($"findClip : {findClip_.name}");
        return findClip_;
        //return null;
    }
    #endregion

    #region Editor Func
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
    #endregion
}

