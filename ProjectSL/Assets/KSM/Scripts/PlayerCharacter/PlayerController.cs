using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;
    [SerializeField]
    private PlayerCharacter playerCharacter;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private AnimationController animationController;

    private float tempMoveSpeed = 5f;

    public CharacterControlProperty controlProperty = new CharacterControlProperty();

    [SerializeField]
    private CharacterController characterController = default;

    
    // 임시
    public Vector3 moveDir;
    public Vector3 inputDir;
    public Move nextMove;
    public Behavior wait;
    bool isMove;

    public GameObject leftArm;
    public GameObject rightArm;
    private void Awake()
    {
        // 컴포넌트 초기화
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController = GetComponent<CharacterController>();
        cameraController = GetComponent<CameraController>();
        animationController = GetComponent<AnimationController>();

        GameManager.Instance.playerLeftArm = leftArm;
        GameManager.Instance.playerRightArm = rightArm;
        GameManager.Instance.player = gameObject;
    }   // Awake()
    // Start is called before the first frame update
    void Start()
    {
        // nextMove = new Move(characterController, Vector3.zero, tempMoveSpeed);
    }   //Start()

    // Update is called once per frame
    void Update()
    {
        // SetMove();
    }   // Update()

    private void FixedUpdate()
    {
        // MoveExecute();
    }   // FixedUpdate()

    /*
    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Vector2 moveInput = new Vector3(input.x, input.y);
        controlProperty.axisValue = moveInput;
        controlProperty.speed = Mathf.Ceil(Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y) / 2f);

        if (input != null)
        {
            inputDir = new Vector3(input.x, 0f, input.y);
            Debug.Log($"SEND_MESSAGE : {input.magnitude}");
        }
    }
    */
    void SetMove()
    {
        
        bool isInputMove = inputDir.magnitude != 0;
        if(isInputMove)
        {
            playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.MOVE);
        }
        else
        {
            playerCharacter.SM_Behavior.ChangeState(EBehaviorStateName.IDLE);
        }

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        moveDir = lookForward * inputDir.z + lookRight * inputDir.x;

        bool isMove = inputDir.magnitude != 0;
        // input값 있으면 moveDirection 계산
        if (isMove)
        {

            // 캐릭터 회전 * 임시일수도 있음
            if (cameraController.CameraState == ECameraState.DEFAULT)
                characterBody.forward = moveDir;
            else if(cameraController.CameraState == ECameraState.LOCKON)
                characterBody.forward = cameraController.cameraArm.forward;

            nextMove = new Move(characterController, moveDir, tempMoveSpeed);
        }
        else 
        {
            wait = new Behavior();
        }
        //controlProperty.axisValue = new Vector2(moveDir.x, moveDir.z);
        //controlProperty.speed = Mathf.Ceil(Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.z) / 2f);

        // Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }   // Move()

    void MoveExecute()
    {
        nextMove.Execute();
    }

    // Legacy Code
    /*
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
    */

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
