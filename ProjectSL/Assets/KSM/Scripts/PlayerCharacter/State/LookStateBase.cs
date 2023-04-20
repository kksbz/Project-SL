using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LookStateBase
{
    protected CameraController controller { get; private set; }
    public LookStateBase(CameraController controller)
    {
        this.controller = controller;
    }

    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
}
