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
    public void AddState(ELookStateName stateName, LookStateBase state)    // * ī�޶� ����
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }
    public LookStateBase GetState(ELookStateName stateName) // * ī�޶� ����
    {
        if (states.TryGetValue(stateName, out LookStateBase state))
        {
            return state;
        }
        return null;
    }
    public void DeleteState(ELookStateName stateName)   // * ī�޶� ����
    {
        if (states.ContainsKey(stateName))
        {
            states.Remove(stateName);
        }
    }
    public void ChangeState(ELookStateName stateName)   // * ī�޶� ����
    {
        // ���� ������ ���� �Լ� ����
        currentState?.OnExitState();
        // ���� ���� ��������
        if (states.TryGetValue(stateName, out LookStateBase nextState))
        {
            currentState = nextState;
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
