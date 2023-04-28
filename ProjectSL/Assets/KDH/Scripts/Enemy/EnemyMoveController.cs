using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveController : GData.IInitialize
{
    NavMeshAgent NavMeshAgent { get; }
    List<Transform> Targets { get; }
    Transform Target { get; }
    void SetSpeed(float newSpeed);
    void SetStop(bool isStopped);
    void Patrol();
    void TargetFollow(Transform newTarget);
    void TargetFollow(Transform newTarget, bool isFollow);
    void Warp();
    void Warp(Vector3 newPos);
    bool IsArrive(float distance);
    bool IsMissed(float distance);
    bool IsNavMeshRangeChecked(float ranged);
    bool IsRangeChecked(float ranged);
}

public class EnemyMoveController : MonoBehaviour, IEnemyMoveController
{
    private NavMeshAgent _navMeshAgent = default;
    private int _index;

    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } private set { _navMeshAgent = value; } }

    /// <summary>
    /// 순찰 지점 List
    /// </summary>
    /// <value></value>
    public List<Transform> Targets { get; private set; }
    public Transform Target { get; private set; }
    public Transform[] targets;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        Targets = new List<Transform>();
        _index = 0;
        foreach (var element in targets)
        {
            Targets.Add(element);
        }
        Target = Targets[_index];
    }

    public void SetSpeed(float newSpeed)
    {
        NavMeshAgent.speed = newSpeed;
    }

    public void SetStop(bool isStopped)
    {
        Debug.Log($"NavMeshAgent isStopped : {NavMeshAgent.isStopped}");
        NavMeshAgent.isStopped = isStopped;
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
            StartCoroutine(MoveDelay(1f));
        }
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
            Target = newTarget;
            NavMeshAgent.SetDestination(newTarget.position);
            NavMeshAgent.isStopped = isFollow;
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

        NavMeshAgent.Warp(randPos);
    }

    public void Warp(Vector3 newPos)
    {
        NavMeshAgent.Warp(newPos);
    }

    /// <summary>
    /// 순찰 시 순찰 지점에 도달한 후 바로 이동하는게 아닌 잠시간에 딜레이를 주고 이동시키기 위해서 작성한 함수
    /// </summary>
    /// <param name="delay">딜레이를 줄 시간 Sec</param>
    /// <returns></returns>
    IEnumerator MoveDelay(float delay)
    {
        // Targets.Enqueue(_target);
        // _target = default;
        // yield return new WaitForSeconds(delay);
        // _target = Targets.Dequeue();
        // TargetFollow(_target);

        _index++;
        if (Targets.Count <= _index)
        {
            _index = 0;
        }
        yield return new WaitForSeconds(delay);
        Target = Targets[_index];
        TargetFollow(Target);
    }

    /// <summary>
    /// 목표에 도달했는지 확인하기 위한 함수
    /// </summary>
    /// <param name="distance">목표와의 거리 해당 거리 만큼 들어오면 도달했다고 판단</param>
    /// <returns></returns>
    public bool IsArrive(float distance)
    {
        if (Target == null || Target == default || distance < NavMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 타겟을 추적 중 타겟을 놓쳤는지 확인하기 위한 함수
    /// </summary>
    /// <param name="distance">인식 범위 / 해당 범위를 벗어나면 놓친것으로 판단</param>
    /// <returns></returns>
    public bool IsMissed(float distance)
    {
        if (Target == null || Target == default || NavMeshAgent.remainingDistance < distance)
        {
            return false;
        }
        else
        {
            return true;
        }
        // if (distance < _navMeshAgent.remainingDistance)
        // {
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }
    }

    /// <summary>
    /// 타겟이 범위 내에 있는지 확인하는 함수 (NavMeshAgent에 RemainingDistance를 사용)
    /// </summary>
    /// <param name="distance"></param>
    /// <returns>
    /// true : 범위 내에 타겟이 존재  false : 범위 밖에 타겟이 존재
    /// </returns>
    public bool IsNavMeshRangeChecked(float ranged)
    {
        if (Target == null || Target == default)
        {
            return false;
        }

        Debug.Log($"ranged : {ranged} / remainingDistance : {NavMeshAgent.remainingDistance}");

        if (ranged < NavMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 타겟이 범위 내에 있는지 확인하는 함수
    /// </summary>
    /// <param name="ranged"></param>
    /// <returns></returns>
    public bool IsRangeChecked(float ranged)
    {
        if (Target == null || Target == default)
        {
            return false;
        }

        float distance_ = Vector3.Distance(transform.position, Target.position);

        if (ranged < distance_)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
