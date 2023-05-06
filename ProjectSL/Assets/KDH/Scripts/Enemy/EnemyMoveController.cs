using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveController : GData.IInitialize
{
    NavMeshAgent NavMeshAgent { get; }
    List<Transform> PatrolPoints { get; }
    Transform Target { get; }
    void SetStoppingDistance(float newDistance);
    void SetSpeed(float newSpeed);
    void SetStop(bool isStopped);
    void SetUpdateRotation(bool isRotation);
    void Patrol();
    void PatrolStop();
    void TargetFollow(Transform newTarget);
    void TargetFollow(Vector3 newPosition);
    void TargetFollow(Transform newTarget, bool isFollow);
    void TargetFollow(Vector3 newPosition, bool isFollow);
    void Warp();
    void Warp(Vector3 newPos);
    bool IsArrive(float ranged);
    bool IsArrivePatrol(float ranged);
    bool IsInRange(float ranged);

    bool IsPositionValid(Vector3 newPosition);
}

public class EnemyMoveController : MonoBehaviour, IEnemyMoveController
{
    private NavMeshAgent _navMeshAgent = default;
    private IEnumerator _moveDelay;
    private int _index;

    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } private set { _navMeshAgent = value; } }

    /// <summary>
    /// 순찰 지점 List
    /// </summary>
    /// <value></value>
    public List<Transform> PatrolPoints { get; private set; }
    public Transform Target { get; private set; }
    [Header("순찰 포인트")]
    public List<Transform> patrolPointsInspector;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        PatrolPoints = new List<Transform>();
        _index = 0;
        if (patrolPointsInspector.IsValidCollection())
        {
            foreach (var element in patrolPointsInspector)
            {
                PatrolPoints.Add(element);
            }
        }
        else
        {
            PatrolPoints.Add(transform);
        }

        Target = PatrolPoints[_index];

        //_moveDelay = MoveDelay(1f);
    }

    public void SetStoppingDistance(float newDistance)
    {
        NavMeshAgent.stoppingDistance = newDistance;
    }

    public void SetSpeed(float newSpeed)
    {
        NavMeshAgent.speed = newSpeed;
    }

    public void SetStop(bool isStopped)
    {
        NavMeshAgent.isStopped = isStopped;
    }

    public void SetUpdateRotation(bool isRotation)
    {
        NavMeshAgent.updateRotation = isRotation;
    }



    /// <summary>
    /// 지정된 대상을 향해서 NavMeshAgent를 사용해서 이동하는 함수
    /// </summary>
    /// <param name="newTarget"></param>
    public void TargetFollow(Transform newTarget)
    {
        if (newTarget == null || newTarget == default)
        {
            /* Do Nothing */
        }
        else
        {
            Target = newTarget;
            NavMeshAgent.SetDestination(newTarget.position);
        }
    }
    public void TargetFollow(Vector3 newPosition)
    {
        NavMeshAgent.SetDestination(newPosition);
        // if (IsPositionReachable(newPosition))
        // {
        //     Debug.Log($"Dodge Debug : {newPosition}");
        //     NavMeshAgent.SetDestination(newPosition);
        // }
        // else
        // {
        //     /*  Do Nothing  */
        // }
    }

    /// <summary>
    /// 지정된 대상을 향해서 NavMeshAgent를 사용해서 이동하는 함수 + isStopped를 조정해서 정지를 시키는 동작 수행
    /// </summary>
    /// <param name="newTarget"></param>
    /// <param name="isFollow"></param>
    public void TargetFollow(Transform newTarget, bool isFollow)
    {
        if (newTarget == null || newTarget == default)
        {
            /* Do Nothing */
        }
        else
        {
            TargetFollow(newTarget);
            SetStop(isFollow);
        }
    }
    public void TargetFollow(Vector3 newPosition, bool isFollow)
    {
        if (IsPositionValid(newPosition))
        {
            TargetFollow(newPosition);
            SetStop(isFollow);
        }
        else
        {
            /*  Do Nothing  */
        }
    }
    const float RANDOM_X_RANGE = 10f;
    const float RANDOM_Y_RANGE = 10f;
    public void Warp()
    {
        float randX_ = Random.Range(transform.position.x - RANDOM_X_RANGE, transform.position.x + RANDOM_X_RANGE);
        float randY_ = Random.Range(transform.position.z - RANDOM_Y_RANGE, transform.position.z + RANDOM_Y_RANGE);

        Vector3 randPos = new Vector3(randX_, 0f, randY_);
        Debug.Log($"Warp Pos : {randPos}");
        if (IsPositionValid(randPos))
        {
            NavMeshAgent.Warp(randPos);
        }
        else
        {
            return;
        }
    }

    public void Warp(Vector3 newPos)
    {
        if (IsPositionValid(newPos))
        {
            NavMeshAgent.Warp(newPos);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 순찰을 위해 지정된 범위를 반복 이동하는 함수
    /// </summary>
    public void Patrol()
    {
        if (Target == null || Target == default)
        {
            /* Do Nothing */
        }
        else
        {
            _moveDelay = MoveDelay(1f);
            StartCoroutine(_moveDelay);
            //StartCoroutine(MoveDelay(1f));
        }
    }

    public void PatrolStop()
    {
        StopCoroutine(_moveDelay);
    }

    /// <summary>
    /// 순찰 시 순찰 지점에 도달한 후 바로 이동하는게 아닌 잠시간에 딜레이를 주고 이동시키기 위해서 작성한 함수
    /// </summary>
    /// <param name="delay">딜레이를 줄 시간 Sec</param>
    /// <returns</returns>
    IEnumerator MoveDelay(float delay)
    {
        // Targets.Enqueue(_target);
        // _target = default;
        // yield return new WaitForSeconds(delay);
        // _target = Targets.Dequeue();
        // TargetFollow(_target);

        _index++;
        Debug.Log($"Patrol Debug : Targets.Count : {PatrolPoints.Count} / currentIndex : {_index} ");
        if (PatrolPoints.Count <= _index)
        {
            _index = 0;
            Debug.Log($"_index = 0");
        }
        Target = PatrolPoints[_index];
        TargetFollow(Target, true);
        yield return new WaitForSeconds(delay);
        TargetFollow(Target, false);
    }

    /// <summary>
    /// 특정 좌표가 현재 NavMeshAgent가 이동할 수 있는 좌표인지 확인하는 함수
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    public bool IsPositionValid(Vector3 newPosition)
    {
        NavMeshHit hit;
        bool isOnNavMesh = NavMesh.SamplePosition(newPosition, out hit, _navMeshAgent.height * 0.5f, NavMesh.AllAreas);

        return isOnNavMesh;
    }


    //  타겟이 유효한지 확인할 Property
    public bool IsValidTarget
    {
        get
        {
            if (Target == null || Target == default)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    //  순찰 지점이 유효한지 확인할 Property
    public bool IsValidTargetPatrol
    {
        get
        {
            return IsValidTarget & 1 < PatrolPoints.Count;
        }
    }

    //  타겟이 플레이어인지 확인할 Property
    public bool IsValidTargetPlayer
    {
        get
        {
            return IsValidTarget & Target.gameObject.layer.Equals(8);
        }
    }

    /// <summary>
    /// NavMeshAgent과 타겟 사이 거리를 확인하는 함수
    /// </summary>
    /// <param name="distance">범위</param>
    /// <returns>범위 내에 타겟이 있다면 : TRUE / 범위 내에 타겟이 없다면 : FALSE</returns>
    public bool IsArrive(float ranged)
    {
        if (!IsValidTarget || ranged < NavMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 순찰 대상이 유효한지 범위 내에 있는지 확인하는 함수
    /// </summary>
    /// <param name="ranged"></param>
    /// <returns></returns>
    public bool IsArrivePatrol(float ranged)
    {
        if (!IsValidTargetPatrol || ranged < NavMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 추적 상태일 때 추적 대상이 유효한지, 추적 대상이 범위 내 있는지 확인하는 함수
    /// </summary>
    /// <param name="distance">범위</param>
    /// <returns>추적 대상이 범위 내 있으면 : TRUE / 추적 대상이 유효하지 않거나 범위 내 존재하지 않으면 : FALSE </returns>
    public bool IsInRange(float ranged)
    {
        Debug.Log($"NavMeshAgent : {NavMeshAgent.destination}");
        if (!IsValidTargetPlayer || ranged < NavMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
