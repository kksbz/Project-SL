using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField]
    private CinemachineBrain cinemachineBrain;
    [SerializeField]
    private CinemachineFreeLook freelookCamera;
    [SerializeField]
    private CinemachineFreeLook lockTargetCamera;

    private void Awake()
    {
        GameObject playerCharacter = GFunc.GetRootObj("PlayerCharacter");
        GameObject playerMeshObj = playerCharacter.FindChildObj("Mesh");
        GameObject targetGroupObj = playerCharacter.FindChildObj("TargetGroup");
        Transform cameraTarget = playerMeshObj.FindChildObj("CameraTarget").transform;
        CinemachineTargetGroup targetGroup = targetGroupObj.GetComponent<CinemachineTargetGroup>();
        freelookCamera.Follow = playerMeshObj.transform;
        freelookCamera.LookAt = cameraTarget;
        lockTargetCamera.Follow = playerMeshObj.transform;
        lockTargetCamera.LookAt = targetGroup.transform;

    }

    public void LockCamera()
    {
        freelookCamera.Priority = 0;
        lockTargetCamera.Priority = 1;
    }
    public void FreeCamera()
    {
        freelookCamera.Priority = 1;
        lockTargetCamera.Priority = 0;
        freelookCamera.gameObject.transform.position = transform.position;
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
