using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : ICommand
{
    // { Move
    protected Rigidbody rigidbody;
    protected CharacterController controller;
    protected Vector3 moveDirection;
    protected float moveSpeed;
    // } Move

    // { PoseAction?
    
    // } PoseAction
    public virtual void Execute()
    {
        /* Do Nothing */
    }
    public virtual void Undo()
    {
        /* Do Nothing */
    }
}
