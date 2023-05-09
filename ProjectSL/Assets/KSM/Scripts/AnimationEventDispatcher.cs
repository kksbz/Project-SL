using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Animations;
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
        //Debug.Log($"{animator.runtimeAnimatorController.animationClips.Length} animation clips.");
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
            AddStartAnimationEvent(clip);
            AddEndAnimationEvent(clip);
        }
    }

    public void AddAnimationStartEndByAnimOV(AnimatorOverrideController animOV)
    {
        for(int i = 0; i < animOV.animationClips.Length; i++)
        {
            AnimationClip clip = animOV.animationClips[i];
            
            if(clip == null)
            {
                continue;
            }
            AddStartAnimationEvent(clip);
            AddEndAnimationEvent(clip);
            //Debug.Log($"Add Animation Event, clip is {clip.name}");
        }
    }

    public void AddAnimationStartEndByAnimOV(RuntimeAnimatorController animOV)
    {
        for (int i = 0; i < animOV.animationClips.Length; i++)
        {
            AnimationClip clip = animOV.animationClips[i];
            if (clip == null)
            {
                continue;
            }
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
        animationEndEvent.time = clip.length - 0.0001f;
        if (clip.name == "ItemAction_Potion")
        {
            //Debug.Log($"clip length : {clip.length}, AnimationEndEventTime = {animationEndEvent.time}");
        }
        animationEndEvent.functionName = "AnimationEndHandler";
        animationEndEvent.stringParameter = clip.name;

        clip.AddEvent(animationEndEvent);
    }


    public void AnimationStartHandler(string name)
    {
        //Debug.Log($"{name} animation start.");
        onAnimationStart?.Invoke(name);
    }
    public void AnimationEndHandler(string name)
    {
        //Debug.Log($"{name} animation end");
        onAnimationEnd?.Invoke(name);
    }
}
