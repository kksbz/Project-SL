using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveController
{
    Queue<Transform> Targets { get; }
    void Init();
    void Move();
    void Stop();
    bool IsArrive();
}

public class EnemyMoveController : MonoBehaviour, IEnemyMoveController
{
    private NavMeshAgent _navMeshAgent = default;
    private Transform _target = default;
    [SerializeField]
    private float _stopDistance;
    public Transform[] targets;
    public Queue<Transform> Targets { get; private set; }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        Targets = new Queue<Transform>();
        foreach (var element in targets)
        {
            Targets.Enqueue(element);
        }
    }

    public void Move()
    {
        if (_target == null || _target == default)
        {
            /* Do Nothing */
        }
        else
        {
            Targets.Enqueue(_target);
            _target = default;
        }
        StartCoroutine(MoveDelay(1f));
    }

    IEnumerator MoveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _target = Targets.Dequeue();
        _navMeshAgent.SetDestination(_target.position);
    }

    public void Stop()
    {

    }

    public bool IsArrive()
    {
        if (_target == null || _target == default || _stopDistance < _navMeshAgent.remainingDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
