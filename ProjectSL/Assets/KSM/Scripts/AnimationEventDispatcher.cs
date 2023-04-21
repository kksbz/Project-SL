using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityAnimationEvent : UnityEvent<string> { };
[RequireComponent(typeof(Animator))]
public class AnimationEventDispatcher : MonoBehaviour
{
    public UnityAnimationEvent onAnimationStart;
    public UnityAnimationEvent onAnimationEnd;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log($"{animator.runtimeAnimatorController.animationClips.Length} animation clips.");
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
            AddStartAnimationEvent(clip);
            AddEndAnimationEvent(clip);
        }
    }

    public void AddStartAnimationEvent(AnimationClip clip)
    {
        AnimationEvent animationStartEvent = new AnimationEvent();
        animationStartEvent.time = 0;
        animationStartEvent.functionName = "AnimationStartHandler";
        animationStartEvent.stringParameter = clip.name;
        clip.AddEvent(animationStartEvent);
    }

    public void AddEndAnimationEvent(AnimationClip clip)
    {
        AnimationEvent animationEndEvent = new AnimationEvent();
        animationEndEvent.time = clip.length;
        animationEndEvent.functionName = "AnimationEndHandler";
        animationEndEvent.stringParameter = clip.name;
        clip.AddEvent(animationEndEvent);
    }


    public void AnimationStartHandler(string name)
    {
        Debug.Log($"{name} animation start.");
        onAnimationStart?.Invoke(name);
    }
    public void AnimationEndHandler(string name)
    {
        Debug.Log($"{name} animation end");
        onAnimationEnd?.Invoke(name);
    }
}
