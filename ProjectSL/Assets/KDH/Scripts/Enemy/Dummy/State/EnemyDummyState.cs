using UnityEngine;

public class Enemy_Dummy_Idle_State : IState
{
    EnemyDummy enemy;
    public Enemy_Dummy_Idle_State(EnemyDummy newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (1 <= enemy.Targets.Count)
        {
            enemy.SetState(new Enemy_Dummy_Patrol_State(enemy));
        }
    }
}

public class Enemy_Dummy_Patrol_State : IState
{
    EnemyDummy enemy;
    public Enemy_Dummy_Patrol_State(EnemyDummy newEnemy)
    {
        enemy = newEnemy;
    }

    public void OnEnter()
    {
        enemy.Move();
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (enemy.IsArrive())
        {
            enemy.Move();
        }
    }
}

public class Enemy_Dummy_Charger_State : IState
{
    EnemyDummy enemy;
    public Enemy_Dummy_Charger_State(EnemyDummy newEnemy)
    {
        enemy = newEnemy;
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
