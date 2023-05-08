using UnityEngine;
using ProjectSL.Enemy;
using System.Collections;

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

public class Boss_Ramapage_BodyTackle_State : IState
{
    private Boss_Rampage _boss;
    private IEnumerator bodyTackleEndCoroutine;
    public Boss_Ramapage_BodyTackle_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled(2);
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("BodyTackle");
        _boss.BodyTackle();
        OnAction();

        AudioClip bodyTackleAudio_ = _boss.FindAudioClip("BodyTackle");

        _boss.SFX_Play(bodyTackleAudio_);

        bodyTackleEndCoroutine = BodyTackleEnd();
        _boss.StartCoroutine(bodyTackleEndCoroutine);
    }

    public void OnExit()
    {
        _boss.StopCoroutine(bodyTackleEndCoroutine);
        _boss.BodyTackleComplete();
        _boss.TargetFollow(_boss.Target, false);
        OnAction();
    }

    public void Update()
    {
        if (_boss.MoveCompleteCheck(0.7f))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }

    IEnumerator BodyTackleEnd()
    {
        yield return new WaitForSeconds(1f);
        _boss.SetState(new Boss_Thought_State(_boss));
    }
}

public class Boss_Rampage_GroundSmash_Start_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_GroundSmash_Start_State(Boss_Rampage newBoss)
    {
        _boss = newBoss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Ground_Smash_Start");

        Vector3 dir_ = (_boss.Target.position - _boss.transform.position).normalized;
        Vector3 targetPos = _boss.Target.transform.position - dir_ * 3f;
        //_boss.Target.TransformPoint(dir_ * 2f);
        Debug.Log($"Ground Smash Start Debug : {targetPos}");
        _boss.Jump(targetPos, _boss.BossStatus.groundSmashMaxHeight);

        int randNum_ = Random.Range(0, 3);


        AudioClip groundSmashAudio_ = _boss.FindAudioClip("GroundSmash");

        _boss.SFX_Play(groundSmashAudio_);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.MoveCompleteCheck(0.7f) && _boss.IsAnimationPlaying("Ground_Smash_Loop"))
        {
            _boss.SetState(new Boss_Rampage_GroundSmash_End_State(_boss));
        }
    }
}

public class Boss_Rampage_GroundSmash_End_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_GroundSmash_End_State(Boss_Rampage newBoss)
    {
        _boss = newBoss;
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled();
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Ground_Smash_End");
    }

    public void OnExit()
    {
        _boss.TargetFollow(_boss.Target, false);
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Ground_Smash_End"))
        {
            //_boss.JumpComplete();
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}

#region Attack A, B, C Pattern
public class Boss_Rampage_Attack_A_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_A_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled(0);
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_A");

        int randNum_ = Random.Range(0, 3);

        AudioClip attackAudio_ = default;
        if (randNum_ == 0)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_1");
        }
        else if (randNum_ == 1)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_2");
        }
        else if (randNum_ == 2)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_3");
        }

        _boss.SFX_Play(attackAudio_);
    }

    public void OnExit()
    {
        _boss.TargetFollow(_boss.Target, false);
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_A"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
public class Boss_Rampage_Attack_B_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_B_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled(1);
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_B");

        int randNum_ = Random.Range(0, 3);

        AudioClip attackAudio_ = default;
        if (randNum_ == 0)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_1");
        }
        else if (randNum_ == 1)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_2");
        }
        else if (randNum_ == 2)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_3");
        }

        _boss.SFX_Play(attackAudio_);
    }

    public void OnExit()
    {
        _boss.TargetFollow(_boss.Target, false);
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_B"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
public class Boss_Rampage_Attack_C_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Attack_C_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
        _boss.NotAttackColliderEnabled(0);
    }

    public void OnEnter()
    {
        _boss.SetTrigger(EnemyDefineData.TRIGGER_ATTACK);
        _boss.SetTrigger("Attack_C");

        int randNum_ = Random.Range(0, 3);

        AudioClip attackAudio_ = default;
        if (randNum_ == 0)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_1");
        }
        else if (randNum_ == 1)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_2");
        }
        else if (randNum_ == 2)
        {
            attackAudio_ = _boss.FindAudioClip("Attack_3");
        }

        _boss.SFX_Play(attackAudio_);
    }

    public void OnExit()
    {
        _boss.TargetFollow(_boss.Target, false);
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Attack_C"))
        {
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
#endregion

#region DodgePattern
public class Boss_Rampage_Dodge_Start_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Start_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Start");

        _boss.StartCoroutine(DodgeDelay());

        int randNum_ = Random.Range(0, 2);

        AudioClip dodgeAudio_ = default;
        if (randNum_ == 0)
        {
            dodgeAudio_ = _boss.FindAudioClip("Dodge_1");
        }
        else if (randNum_ == 1)
        {
            dodgeAudio_ = _boss.FindAudioClip("Dodge_2");
        }

        _boss.SFX_Play(dodgeAudio_);
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_Start"))
        {
            _boss.SetState(new Boss_Rampage_Dodge_Mid_State(_boss));
        }
    }

    IEnumerator DodgeDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 targetPos = _boss.transform.position - _boss.transform.forward * 8f;
        targetPos.y = _boss.transform.position.y;
        _boss.Jump(targetPos, _boss.BossStatus.dodgeMaxHeight);
    }
}

public class Boss_Rampage_Dodge_Mid_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Mid_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Mid");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_Mid"))
        {
            _boss.SetState(new Boss_Rampage_Dodge_Loop_State(_boss));
        }
    }
}

public class Boss_Rampage_Dodge_Loop_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_Loop_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_Loop");
    }

    public void OnExit()
    {
    }

    public void Update()
    {
        if (_boss.MoveCompleteCheck(0.5f))
        {
            _boss.SetState(new Boss_Rampage_Dodge_End_State(_boss));
        }
    }
}

public class Boss_Rampage_Dodge_End_State : IState
{
    private Boss_Rampage _boss;
    public Boss_Rampage_Dodge_End_State(Boss_Rampage boss)
    {
        _boss = boss;
    }
    public void OnAction()
    {
    }

    public void OnEnter()
    {
        _boss.SetTrigger("Dodge_End");
    }

    public void OnExit()
    {
        _boss.TargetFollow(_boss.Target, false);
    }

    public void Update()
    {
        if (_boss.IsAnimationEnd("Dodge_End"))
        {
            _boss.JumpComplete();
            _boss.SetState(new Boss_Thought_State(_boss));
        }
    }
}
#endregion