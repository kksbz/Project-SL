using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveController : GData.IInitialize
{
    Queue<Transform> Targets { get; }
    void SetSpeed(float newSpeed);
    void Patrol();
    void TargetFollow(Transform newTarget);
    bool IsArrive(float distance);
    bool IsMissed(float distance);
}

public class EnemyMoveController : MonoBehaviour, IEnemyMoveController
{
    private NavMeshAgent _navMeshAgent = default;
    private Transform _target = default;
    public Transform[] targets;

    /// <summary>
    /// 순찰 지점 Queue
    /// </summary>
    /// <value></value>
    public Queue<Transform> Targets { get; private set; }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        Targets = new Queue<Transform>();
        foreach (var element in targets)
        {
            Targets.Enqueue(element);
        }
        _target = Targets.Dequeue();
    }

    public void SetSpeed(float newSpeed)
    {
        _navMeshAgent.speed = newSpeed;
    }

    /// <summary>
    /// 순찰을 위해 지정된 범위를 반복 이동하는 함수
    /// </summary>
    public void Patrol()
    {
        if (_target == null || _target == default)
        {
            /* Do Nothing */
        }
        else
        {
            StartCoroutine(MoveDelay(0f));
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
            _navMeshAgent.SetDestination(newTarget.position);
        }
    }

    /// <summary>
    /// 순찰 시 순찰 지점에 도달한 후 바로 이동하는게 아닌 잠시간에 딜레이를 주고 이동시키기 위해서 작성한 함수
    /// </summary>
    /// <param name="delay">딜레이를 줄 시간 Sec</param>
    /// <returns></returns>
    IEnumerator MoveDelay(float delay)
    {
        Targets.Enqueue(_target);
        _target = default;
        yield return new WaitForSeconds(delay);
        _target = Targets.Dequeue();
        TargetFollow(_target);
    }

    /// <summary>
    /// 목표에 도달했는지 확인하기 위한 함수
    /// </summary>
    /// <param name="distance">목표와의 거리 해당 거리 만큼 들어오면 도달했다고 판단</param>
    /// <returns></returns>
    public bool IsArrive(float distance)
    {
        Debug.Log($"test (distance : {distance} / remainingDistance : {_navMeshAgent.remainingDistance})");
        if (_target == null || _target == default || distance < _navMeshAgent.remainingDistance)
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
        if (_target == null || _target == default || _navMeshAgent.remainingDistance < distance)
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


}
