public class StateMachine
{
    IState currentState;

    /// <summary>
    /// 상태를 변경하는 함수
    /// </summary>
    /// <param name="newState">변경할 상태</param>
    public void SetState(IState newState)
    {
        if (currentState == null || currentState == default)
        {
            currentState = newState;
            OnEnter();
        }
        else
        {
            OnExit();
            currentState = newState;
            OnEnter();
        }
    }

    public void OnEnter()
    {
        currentState.OnEnter();
    }
    public void Update()
    {
        currentState.Update();
    }
    public void OnExit()
    {
        currentState.OnExit();
    }
}