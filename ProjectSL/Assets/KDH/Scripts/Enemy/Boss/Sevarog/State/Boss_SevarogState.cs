/*

    보스 구현 
    패턴 구성
    1. Idle 상태
    2. 플레이어 탐색 상태(순찰)
    3. 플레이어 추적 상태
    4. 플레이어 대치 상태(플레이어가 일정 범위 내에 있을 경우 - 추적이 필요하지 않은 경우)
        4-1. 
    5. 플레이어 공격 상태
        5-1. 하위 공격 패턴 구성
*/

public class Boss_Sevarog_Idle_State : IState
{
    private Boss_Sevarog _boss;
    public Boss_Sevarog_Idle_State(Boss_Sevarog newBoss)
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