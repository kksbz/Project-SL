using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent = default;
    private List<Transform> targets = default;
    public List<Transform> Targets { get { return targets; } private set { targets = value; } }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        TryGetComponent<NavMeshAgent>(out navMeshAgent);
    }

    public void Move()
    {

    }

    public void Stop()
    {

    }

}
