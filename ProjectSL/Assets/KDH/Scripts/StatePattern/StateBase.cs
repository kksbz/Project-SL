public interface IState
{
    void OnEnter();
    void Update();
    void OnExit();
    void OnAction();
}

