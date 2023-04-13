using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAnimator : GData.IInitialize
{
    Animator Animator { get; }
    AnimatorStateInfo CurrentStateInfo { get; }
    void SetTrigger(string parameter);
    void SetBool(string parameter, bool value);
    void SetFloat(string parameter, float value);
    void SetInt(string parameter, int value);

}

public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
{
    private Animator _animator;
    private AnimatorStateInfo _currentStateInfo;
    public Animator Animator { get { return _animator; } private set { _animator = value; } }

    public AnimatorStateInfo CurrentStateInfo { get { return _animator.GetCurrentAnimatorStateInfo(0); } }

    public void Init()
    {
        TryGetComponent<Animator>(out _animator);
    }

    public void SetTrigger(string parameter)
    {
        Animator.SetTrigger(parameter);
    }
    public void SetBool(string parameter, bool value)
    {
        Animator.SetBool(parameter, value);
    }
    public void SetFloat(string parameter, float value)
    {
        Animator.SetFloat(parameter, value);
    }
    public void SetInt(string parameter, int value)
    {
        Animator.SetInteger(parameter, value);
    }
}