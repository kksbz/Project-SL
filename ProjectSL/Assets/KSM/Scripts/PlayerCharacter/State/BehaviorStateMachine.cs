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
    public BehaviorStateBase _currentState;

    // Legacy Code
    public BehaviorStateBase currentState { get; private set; }
    public EBehaviorStateName currentStateName { get; private set; }
    private Dictionary<EBehaviorStateName, BehaviorStateBase> states = new Dictionary<EBehaviorStateName, BehaviorStateBase>();
    public BehaviorStateMachine(EBehaviorStateName stateName, BehaviorStateBase state)
    {
        AddState(stateName, state);
        currentState = GetState(stateName);
        currentStateName = stateName;
    }
    public void AddState(EBehaviorStateName stateName, BehaviorStateBase state) // * �ൿ ����
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }

    // ���� ��������
    public BehaviorStateBase GetState(EBehaviorStateName stateName) // * �ൿ ����
    {
        if (states.TryGetValue(stateName, out BehaviorStateBase state))
        {
            return state;
        }
        return null;
    }
    // ���� ����
    public void DeleteState(EBehaviorStateName stateName)   // * �ൿ ����
    {
        if (states.ContainsKey(stateName))
        {
            states.Remove(stateName);
        }
    }
    // ���� ����
    public void ChangeState(EBehaviorStateName stateName)   // * �ൿ ����
    {
        if (currentStateName == stateName)
            return;
        // ���� ������ ���� �Լ� ����
        currentState?.OnExitState();
        // ���� ���� ��������
        if (states.TryGetValue(stateName, out BehaviorStateBase nextState))
        {
            currentState = nextState;
            currentStateName = stateName;
        }
        // �ٲ� ������ ���� �Լ� ����
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
