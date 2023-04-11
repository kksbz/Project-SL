using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : ICommand
{
    // { Move
    protected CharacterController controller;
    protected Vector3 moveDirection;
    protected float moveSpeed;
    // } Move
    public virtual void Execute()
    {

    }
    public virtual void Undo()
    {

    }
}
