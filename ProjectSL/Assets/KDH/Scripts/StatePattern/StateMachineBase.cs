using UnityEngine;

public interface IStateMachine
{
    IState CurrentState { get; }
    IState PreviousState { get; }
    void SetState(IState newState);
    void OnEnter();
    void Update();
    void OnExit();
    void OnAction();
}

public class StateMachineBase : IStateMachine
{
    public IState CurrentState { get; protected set; }
    public IState PreviousState { get; protected set; }

    /// <summary>
    /// 상태를 변경하는 함수
    /// </summary>
    /// <param name="newState">변경할 상태</param>
    public void SetState(IState newState)
    {
        if (CurrentState == null || CurrentState == default)
        {
            CurrentState = newState;
            OnEnter();
        }
        else
        {
            OnExit();
            PreviousState = CurrentState;
            CurrentState = newState;
            OnEnter();
        }
    }

    public void OnEnter()
    {
        CurrentState.OnEnter();
    }
    public void Update()
    {
        Debug.Log($"Current State : {CurrentState.ToString()}");
        CurrentState.Update();
    }
    public void OnExit()
    {
        CurrentState.OnExit();
    }

    public void OnAction()
    {
        CurrentState.OnAction();
    }
}