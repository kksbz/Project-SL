using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELookStateName
{
    NONE = -1,
    FREELOOK,
    LOCKON
}

public class LookStateMachine
{
    public LookStateBase currentState { get; private set; }
    private Dictionary<ELookStateName, LookStateBase> states = new Dictionary<ELookStateName, LookStateBase>();

    public LookStateMachine(ELookStateName stateName, LookStateBase state)
    {
        AddState(stateName, state);
        currentState = GetState(stateName);
    }
    public void AddState(ELookStateName stateName, LookStateBase state)    // * 카메라 상태
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }
    public LookStateBase GetState(ELookStateName stateName) // * 카메라 상태
    {
        if (states.TryGetValue(stateName, out LookStateBase state))
        {
            return state;
        }
        return null;
    }
    public void DeleteState(ELookStateName stateName)   // * 카메라 상태
    {
        if (states.ContainsKey(stateName))
        {
            states.Remove(stateName);
        }
    }
    public void ChangeState(ELookStateName stateName)   // * 카메라 상태
    {
        // 현재 상태의 종료 함수 실행
        currentState?.OnExitState();
        // 다음 상태 가져오기
        if (states.TryGetValue(stateName, out LookStateBase nextState))
        {
            currentState = nextState;
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
