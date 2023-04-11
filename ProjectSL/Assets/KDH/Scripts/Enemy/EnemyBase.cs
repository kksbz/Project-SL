using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [Tooltip("Enemy의 Status")]
    [SerializeField]
    protected EnemyStatus status = default;
    protected EnemyStateMachine stateMachine = default;
    protected EnemyMoveController moveController = default;

    #region Property
    public EnemyStatus Status { get { return status; } protected set { status = value; } }
    public EnemyStateMachine StateMachine { get { return stateMachine; } protected set { stateMachine = value; } }
    public EnemyMoveController MoveController { get { return moveController; } protected set { moveController = value; } }
    public List<Transform> Targets { get { return MoveController.Targets; } }
    #endregion

    protected void Start()
    {
        Init();
    }

    protected void Init()
    {
        StateMachine = new EnemyStateMachine();
        TryGetComponent<EnemyMoveController>(out moveController);

        MoveController.Init();
    }

    protected void Update()
    {
        StateMachine.Update();
    }

    #region StateMachineWarpping
    public void SetState(IState newState)
    {
        StateMachine.SetState(newState);
    }
    #endregion

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    public void FindVisibleTargets()
    {

    }


}
