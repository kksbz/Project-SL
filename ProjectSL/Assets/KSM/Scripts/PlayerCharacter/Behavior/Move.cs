using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Behavior
{
    
    public Move(Rigidbody rigidbody, Vector3 moveDirection, float moveSpeed)
    {
        this.rigidbody = rigidbody;
        this.moveDirection = moveDirection;
        this.moveSpeed = moveSpeed;
    }
    public override void Execute()
    {
        // base.Execute();
        // Debug.Log($"moveDirection : {moveDirection}");
        rigidbody.velocity = moveDirection * moveSpeed;
        // rigidbody.MovePosition((moveDirection * moveSpeed).normalized);
        // rigidbody.AddForce(moveDirection);
        //controller.SimpleMove(moveDirection * moveSpeed);
    }
    public override void Undo()
    {
        //base.Undo();
        controller.Move((moveDirection * -1) * moveSpeed);
    }
}
