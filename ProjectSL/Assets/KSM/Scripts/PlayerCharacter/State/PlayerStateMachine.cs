using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;
    PlayerInput _playerInput;
    Transform _characterBody;

    // 플레이어 컴포넌트
    PlayerCharacter     _playerCharacter;
    PlayerController    _playerController;
    CameraController    _cameraController;
    CombatController    _combatController;
    AnimationController _animationController;

    // 애니메이션 컨트롤 변수 클래스
    CharacterControlProperty _controlProperty;


    // 무브 입력
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;
    bool _isMovementPressed;
    bool _isRunPressed;
    bool _isWalkPressed;

    // 상수
    int _zero = 0;

    // State Var
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // Behavior Command
    Behavior _nextBehavior;

    // getter and setter
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public AnimationController AnimationController { get { return _animationController; } }
    public Animator CharacterAnimator { get { return _animator; } }
    public CharacterControlProperty ControlProperty { get { return _controlProperty; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsWalkPressed { get { return _isWalkPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } }
    public Behavior NextBehavior { get { return _nextBehavior; } set { _nextBehavior = value; } }

    private void Awake()
    {
        // 인풋, 컴포넌트 초기화
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _characterBody = gameObject.FindChildObj("Mesh").transform;
        _animator = _characterBody.gameObject.GetComponent<Animator>();

        _playerCharacter    = GetComponent<PlayerCharacter>();
        _playerController   = GetComponent<PlayerController>();
        _cameraController   = GetComponent<CameraController>();
        _combatController   = GetComponent<CombatController>();
        _animationController= GetComponent<AnimationController>();

        _controlProperty = _playerController.controlProperty;

        Debug.Log("Player State Machine : 컴포넌트 초기화");

        // 스테이트 초기화
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        Debug.Log("Player State Machine : 스테이트 초기화");

        // 플레이어 입력 콜백 설정
        _playerInput.PlayerCharacterInput.Move.started      += OnMovementInput;
        _playerInput.PlayerCharacterInput.Move.canceled     += OnMovementInput;
        _playerInput.PlayerCharacterInput.Move.performed    += OnMovementInput;
        _playerInput.PlayerCharacterInput.Walk.started  += OnWalk;
        _playerInput.PlayerCharacterInput.Walk.canceled += OnWalk;
        _playerInput.PlayerCharacterInput.Run.started   += OnRun;
        _playerInput.PlayerCharacterInput.Run.canceled  += OnRun;
        Debug.Log("Player State Machine : 인풋 바인딩");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
    }

    public void SetMoveDirection()
    {
        Vector3 inputDir = new Vector3(_currentMovementInput.x, 0f, _currentMovementInput.y);

        _controlProperty.axisValue = new Vector2(inputDir.x, inputDir.z);
        _controlProperty.speed = Mathf.Ceil(Mathf.Abs(inputDir.x) + Mathf.Abs(inputDir.z) / 2f);
        Transform cameraArm = _cameraController.cameraArm;
        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        _currentMovement = lookForward * inputDir.z + lookRight * inputDir.x;

        // 캐릭터 회전 * 임시일수도 있음
        if (_cameraController.CameraState == ECameraState.DEFAULT || _isRunPressed)
            _characterBody.forward = _currentMovement;
        else if (_cameraController.CameraState == ECameraState.LOCKON)
            _characterBody.forward = _cameraController.cameraArm.forward;

        _appliedMovement = _currentMovement;
    }

    // 입력 콜백 함수
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
    }
    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
    void OnWalk(InputAction.CallbackContext context)
    {
        _isWalkPressed = context.ReadValueAsButton();
    }
    void OnRightArmAction(InputAction.CallbackContext context)
    {

    }
    void OnLeftArmAction(InputAction.CallbackContext context)
    {

    }
    void OnRoll(InputAction.CallbackContext context)
    {

    }
    private void OnEnable()
    {
        _playerInput.PlayerCharacterInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.PlayerCharacterInput.Disable();
    }
}
