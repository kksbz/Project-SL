public class BossBase : EnemyBase
{
    protected override void Init()
    {
        base.Init();
        SetState(new Boss_Idle_State(this));
    }
}