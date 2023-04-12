using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveController : GData.IInitialize
{
    Queue<Transform> Targets { get; }
    void Patrol();
    void TargetFollow(Transform newTarget);
    bool IsArrive(float distance);
    bool IsMissed(float distance);
}

public class EnemyMoveController : MonoBehaviour, IEnemyMoveController
{
    private NavMeshAgent _navMeshAgent = default;
    private Transform _target = default;
    [SerializeField]
    private float _stopDistance;
    public Transform[] targets;
    public Queue<Transform> Targets { get; private set; }

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

    public void Patrol()
    {
        if (_target == null || _target == default)
        {
            /* Do Nothing */
        }
        else
        {
            StartCoroutine(MoveDelay(2f));
        }
    }

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

    IEnumerator MoveDelay(float delay)
    {
        Targets.Enqueue(_target);
        _target = default;
        yield return new WaitForSeconds(delay);
        _target = Targets.Dequeue();
        TargetFollow(_target);
    }

    public bool IsArrive(float distance)
    {
        if (_target == null || _target == default || distance < _navMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsMissed(float distance)
    {
        if (distance < _navMeshAgent.remainingDistance)
        {
            Debug.Log($"놓친거 같다...");
            return true;
        }
        else
        {
            return false;
        }
    }


}
