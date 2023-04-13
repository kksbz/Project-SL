using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;

    private CameraController cameraController;
    private AnimationController animationController;

    private float tempMoveSpeed = 5f;

    [SerializeField]
    private CharacterController characterController = default;
    // 임시
    
    private void Awake()
    {
        // 컴포넌트 초기화
        characterController = GetComponent<CharacterController>();
        cameraController = GetComponent<CameraController>();
        animationController = GetComponent<AnimationController>();
    }   // Awake()
    // Start is called before the first frame update
    void Start()
    {

    }   //Start()

    // Update is called once per frame
    void Update()
    {
        Move();
    }   // Update()

    void Move()
    {
        Vector2 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Transform cameraArm = cameraController.cameraArm;
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            if(cameraController.CameraState == ECameraState.DEFAULT)
                characterBody.forward = moveDir;
            else if(cameraController.CameraState == ECameraState.LOCKON)
                characterBody.forward = cameraController.cameraArm.forward;
            Move move = new Move(characterController, moveDir, tempMoveSpeed);
            move.Execute();
        }
        else 
        {

        }
        animationController.AxisValue = moveInput;
        animationController.Speed = Mathf.Ceil(Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y) / 2f);
        // Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }   // Move()

    /*
    void LockOn()
    {
        // 임시
        if(Input.GetMouseButtonDown(3))
        {
            switch (cameraState)
            {
                case ECameraState.DEFAULT:
                    break;
                case ECameraState.LOCKON:
                    break;
                default:
                    break;
            }
        }
    }
    */
}
