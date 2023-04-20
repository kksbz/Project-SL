using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseAction : Behavior
{
    private Animator animator;
    private string stateName;
    private int layerIndex;
    private float normalizeTime;
    private AnimatorOverrideController animOV;
    public PoseAction(Animator animator, string animStateName, int layerIdx = 0, float time = 0f, AnimatorOverrideController animatorOV = null)
    {
        this.animator = animator;
        this.stateName = animStateName;
        this.layerIndex = layerIdx;
        this.normalizeTime = time;
        this.animOV = animatorOV;
    }
    public override void Execute()
    {
        if(animOV != null)
        {
            animator.runtimeAnimatorController = animOV;
        }
        animator.Play("Attack", layerIndex, normalizeTime);
    }
    public override void Undo()
    {
        /* Do Nothing */
    }
}
