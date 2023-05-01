using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSL.Enemy
{
    public interface IEnemyAnimator : GData.IInitialize
    {
        Animator Animator { get; }
        AnimatorStateInfo CurrentStateInfo { get; }
        void SetTrigger(string parameter);
        void SetBool(string parameter, bool value);
        void SetFloat(string parameter, float value);
        void SetInt(string parameter, int value);

        /// <summary>
        /// 반복되는 애니메이션이 한번 종료된 시점에 AnimationEvent로 호출할 함수
        /// </summary>
        /// <returns></returns>
        void AnimationComplete();

        /// <summary>
        /// 반복되지 않는 애니메이션이 종료된 시점을 확인할 함수
        /// </summary>
        /// <returns></returns>
        bool IsAnimationEnd();
        bool IsAnimationEnd(string animationName);
        bool IsAnimationPlaying(string animationName);

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

        public void AnimationComplete()
        {
        }

        public bool IsAnimationEnd()
        {
            if (1f <= CurrentStateInfo.normalizedTime && !CurrentStateInfo.loop)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAnimationEnd(string animationName)
        {

            if (!CurrentStateInfo.IsName(animationName))
            {
                return false;
            }

            if (1f <= CurrentStateInfo.normalizedTime && !CurrentStateInfo.loop)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAnimationPlaying(string animationName)
        {
            if (CurrentStateInfo.IsName(animationName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}