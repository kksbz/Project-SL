using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDebugger : MonoBehaviour
{
    [SerializeField]
    private LineRenderer characterDirectionLine;
    [SerializeField]
    private LineRenderer cameraDirectionLine;
    [SerializeField]
    private LineRenderer inputDirectionLine;

    private Vector3 characterDirection;
    private Vector3 cameraDirection;
    private Vector3 inputDirection;

    [SerializeField]
    PlayerController targetController;
    [SerializeField]
    Transform cameraArm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetDirections();
        SetLine_CharacterDirection();
        SetLine_CameraDirection();
        SetLine_InputDirection();
    }
    void SetDirections()
    {
        Vector2 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            inputDirection = moveDir.normalized;
        }
        
        // cameraDirection = Camera.main.transform.forward;
        cameraDirection = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
    }

    void SetLine_CharacterDirection()
    {
        characterDirectionLine.SetPosition(0, transform.position);
        characterDirectionLine.SetPosition(1, transform.position + Vector3.up);

    }
    void SetLine_CameraDirection()
    {
        cameraDirectionLine.SetPosition(0, transform.position);
        cameraDirectionLine.SetPosition(1, transform.position + cameraDirection);
    }
    void SetLine_InputDirection()
    {
        inputDirectionLine.SetPosition(0, transform.position);
        inputDirectionLine.SetPosition(1, transform.position + inputDirection);
    }
}
