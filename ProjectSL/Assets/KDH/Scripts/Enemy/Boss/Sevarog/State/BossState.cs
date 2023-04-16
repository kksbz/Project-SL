/*

    보스 구현 
    기본 베이스 패턴 구성
    1. Idle 상태
    2. 플레이어 탐색 상태(순찰)
    3. 플레이어 추적 상태
    4. 플레이어 대치 상태(플레이어가 일정 범위 내에 있을 경우 - 추적이 필요하지 않은 경우)
        4-1. 
    5. 플레이어 공격 상태
        5-1. 하위 공격 패턴 구성

    보스 패턴 기획
    1. Idle 상태 (아무런 동작 없이 플레이어가 인식 범위 내로 들어올 때까지 대기)
    2. 

*/

/// <summary>
/// 인트로 애니메이션을 재생하기 위한 상태 최초 1회만 동작할 예정
/// </summary>
public class Boss_Intro_State : IState
{
    private BossBase _boss;
    public Boss_Intro_State(BossBase newBoss)
    {
        _boss = newBoss;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}

/// <summary>
/// Idl 상태 플레이어를 조우하기 전 상태
/// </summary>
public class Boss_Idle_State : IState
{
    private BossBase _boss;
    public Boss_Idle_State(BossBase newBoss)
    {
        _boss = newBoss;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}

/// <summary>
/// 대치 상태 플레이어를 주기적으로 쫓아가거나 바라보다 플레이어가 공격 범위 내에 있다면 공격 상태로 전환
/// </summary>
public class Boss_Confrontation_State : IState
{
    private BossBase _boss;
    public Boss_Confrontation_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}

/// <summary>
/// 공격 상태 일정 확률로 정해진 공격 패턴을 수행 시킬 예정
/// </summary>
public class Boss_Attack_State : IState
{
    private BossBase _boss;
    public Boss_Attack_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}

/// <summary>
/// 그로기 상태 우선 상태 제작 후 구현은 미정
/// </summary>
public class Boss_Groggy_State : IState
{
    private BossBase _boss;
    public Boss_Groggy_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}

/// <summary>
/// Die 상태 보스가 죽었을 때 전환될 예정
/// </summary>
public class Boss_Die_State : IState
{
    private BossBase _boss;
    public Boss_Die_State(BossBase newBoss)
    {
        _boss = newBoss;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Update()
    {
    }
}