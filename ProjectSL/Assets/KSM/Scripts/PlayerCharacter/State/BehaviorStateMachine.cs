using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBehaviorStateName
{
    NONE = -1,
    IDLE,
    MOVE,
    ATTACK
}

public class BehaviorStateMachine
{
    public BehaviorStateBase currentState { get; private set; }
    public EBehaviorStateName currentStateName { get; private set; }
    private Dictionary<EBehaviorStateName, BehaviorStateBase> states = new Dictionary<EBehaviorStateName, BehaviorStateBase>();
    public BehaviorStateMachine(EBehaviorStateName stateName, BehaviorStateBase state)
    {
        AddState(stateName, state);
        currentState = GetState(stateName);
        currentStateName = stateName;
    }
    public void AddState(EBehaviorStateName stateName, BehaviorStateBase state) // * 행동 상태
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }

    // 상태 가져오기
    public BehaviorStateBase GetState(EBehaviorStateName stateName) // * 행동 상태
    {
        if (states.TryGetValue(stateName, out BehaviorStateBase state))
        {
            return state;
        }
        return null;
    }
    // 상태 삭제
    public void DeleteState(EBehaviorStateName stateName)   // * 행동 상태
    {
        if (states.ContainsKey(stateName))
        {
            states.Remove(stateName);
        }
    }
    // 상태 변경
    public void ChangeState(EBehaviorStateName stateName)   // * 행동 상태
    {
        if (currentStateName == stateName)
            return;
        // 현재 상태의 종료 함수 실행
        currentState?.OnExitState();
        // 다음 상태 가져오기
        if (states.TryGetValue(stateName, out BehaviorStateBase nextState))
        {
            currentState = nextState;
            currentStateName = stateName;
        }
        // 바뀐 상태의 진입 함수 실행
        currentState?.OnEnterState();
    }
    public void UpdateState()
    {
        currentState?.OnUpdateState();
    }
    public void FixedUpdateState()
    {
        currentState?.OnFixedUpdateState();
    }
}
