using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform playerCharacter;
    [SerializeField]
    private Transform characterRoot;
    [SerializeField]
    private CharacterController characterController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    /*
    private void OnAnimatorMove()
    {
        if(animator == null)
        {
            return;
        }
        Debug.Log($"animator deltaposition : {animator.deltaPosition}");
        Vector3 animDelta = animator.deltaPosition;
        // Vector3 newDeltaPosition = new Vector3(animDelta.x, animDelta.z, -animDelta.y);
        // transform.position -= animDelta / 2f;
        //playerCharacter.transform.position += animDelta;
        characterController.Move(animDelta);
        
    }
    */

}
