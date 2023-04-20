using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorStateBase
{
    protected PlayerController playerController { get; set; }
    public BehaviorStateBase(PlayerController controller)
    {
        this.playerController = controller;
    }
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
}
