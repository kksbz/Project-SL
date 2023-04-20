using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Behavior
{
    
    public Move(CharacterController controller, Vector3 moveDirection, float moveSpeed)
    {
        this.controller = controller;
        this.moveDirection = moveDirection;
        this.moveSpeed = moveSpeed;
    }
    public override void Execute()
    {
        // base.Execute();
        //Debug.Log($"moveDirection : {moveDirection}");
        controller.SimpleMove(moveDirection * moveSpeed);
    }
    public override void Undo()
    {
        //base.Undo();
        controller.Move((moveDirection * -1) * moveSpeed);
    }
}
