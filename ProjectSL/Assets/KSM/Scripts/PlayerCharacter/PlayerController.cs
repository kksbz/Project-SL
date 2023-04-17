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

    public CharacterControlProperty controlProperty = new CharacterControlProperty();

    [SerializeField]
    private CharacterController characterController = default;
    // 임시
    
    private void Awake()
    {
        // 컴포넌트 초기화
        // characterController = GetComponent<CharacterController>();
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
        Vector3 moveDir = MoveInput();

        bool isMove = moveDir.magnitude != 0;
        if (isMove)
        {
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
        // Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }   // Move()

    Vector3 MoveInput()
    {
        Vector2 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controlProperty.axisValue = moveInput;
        controlProperty.speed = Mathf.Ceil(Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y) / 2f);
        Transform cameraArm = cameraController.cameraArm;

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        controlProperty.inputDirection = moveDir;
        return moveDir;

    }

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
