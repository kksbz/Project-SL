using UnityEngine;

public class TransitionReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime > 0)
        {
            animator.Play(stateInfo.fullPathHash, layerIndex, 0f);
        }
    }
}