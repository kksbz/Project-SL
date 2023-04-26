public class Boss_Rampage_RockRaise_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_RockRaise_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Attack");
        _boss.SetTrigger("RockRaise");

        _boss.RockRaise();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Raise"))
        {
            _boss.SetState(new Boss_Rampage_RockThrow_Staet(_boss));
        }
    }
}

public class Boss_Rampage_RockThrow_Staet : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_RockThrow_Staet(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("RockThrow");

        _boss.RockThrow();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Rock_Throw"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}