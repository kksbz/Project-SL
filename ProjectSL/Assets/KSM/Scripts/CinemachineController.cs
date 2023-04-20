using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineBrain cinemachineBrain;
    [SerializeField]
    private CinemachineFreeLook freelockCamera;
    [SerializeField]
    private CinemachineFreeLook lockTargetCamera;

    public void LockCamera()
    {
        freelockCamera.Priority = 0;
        lockTargetCamera.Priority = 1;
    }
    public void FreeCamera()
    {
        freelockCamera.Priority = 1;
        lockTargetCamera.Priority = 0;
        freelockCamera.gameObject.transform.position = transform.position;
    }
    public void SetLookAt(Transform newTargetTR)
    {
        lockTargetCamera.LookAt = newTargetTR;
    }
    public void FollowTarget(Transform newFollowTR)
    {
        lockTargetCamera.Follow = newFollowTR;
    }

}
