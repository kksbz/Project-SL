using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorMove : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform playerCharacter;
    [SerializeField]
    private CharacterController characterController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    
    private void OnAnimatorMove()
    {
        if(animator == null)
        {
            return;
        }
        Debug.Log($"animator deltaposition : {animator.deltaPosition}");
        Vector3 animDelta = animator.deltaPosition;
        // transform.position -= animDelta / 2f;
        //playerCharacter.transform.position += animDelta;
        characterController.Move(animDelta);
        
    }
}
