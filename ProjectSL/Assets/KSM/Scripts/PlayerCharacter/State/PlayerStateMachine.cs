using Cinemachine;
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
    CinemachineInputProvider _cameraInput;

    [SerializeField]
    Transform _characterBody;

    // �÷��̾� ������Ʈ
    PlayerCharacter     _playerCharacter;
    PlayerController    _playerController;
    CameraController    _cameraController;
    CombatController    _combatController;
    AnimationController _animationController;
    EquipmentController _equipmentController;
    AnimationEventDispatcher _animationEventDispatcher;

    // �ִϸ��̼� ��Ʈ�� ���� Ŭ����
    CharacterControlProperty _controlProperty;


    // ���� �Է�
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;
    Vector3 _currentDirection;
    bool _isMovementPressed;
    bool _isRunPressed;
    bool _isWalkPressed;
    float runPressedRate = 0.5f;
    

    // ���
    int _zero = 0;

    // ���� �Է�
    bool _isAttackPressed;
    bool _isGuardPressed;
    bool _isRollPressed;
    bool _isBackStepPressed;
    bool _isSwitchingArmPressed;
    float _dodgePressedRate = 0.5f;
    float _dodgeStartTime = 0f;

    bool _hitFlag;
    bool _blockFlag;
    // State Var
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // Behavior Command
    Behavior _nextBehavior;

    //
    bool _canRotate;
    //

    // getter and setter
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public AnimationController AnimationController { get { return _animationController; } }
    public CombatController CombatController { get { return _combatController; } }
    public EquipmentController EquipmentController { get { return _equipmentController; } }
    public Animator CharacterAnimator { get { return _animator; } }
    public CharacterControlProperty ControlProperty { get { return _controlProperty; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsWalkPressed { get { return _isWalkPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool IsAttackPressed {  get { return _isAttackPressed; } }
    public bool IsGuardPressed { get { return _isGuardPressed; } }
    public bool IsRollPressed { get { return _isRollPressed; } set { _isRollPressed = value; } }
    public bool IsBackStepPressed { get { return _isBackStepPressed; } set { _isBackStepPressed = value; } }
    public bool HitFlag { get { return _hitFlag; } set { _hitFlag = value; } }
    public bool BlockFlag { get { return _blockFlag; } set { _blockFlag = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } }
    public Vector3 AppliedMovement { get { return _appliedMovement; } }
    public Behavior NextBehavior { get { return _nextBehavior; } set { _nextBehavior = value; } }

    private void Awake()
    {
        // ��ǲ, ������Ʈ �ʱ�ȭ
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _characterBody = gameObject.FindChildObj("Mesh").transform;
        _animator = _characterBody.gameObject.GetComponent<Animator>();

        _playerCharacter    = GetComponent<PlayerCharacter>();
        _playerController   = GetComponent<PlayerController>();
        _cameraController   = GetComponent<CameraController>();
        _combatController   = GetComponent<CombatController>();
        _animationController= GetComponent<AnimationController>();
        _equipmentController= GetComponent<EquipmentController>();
        _animationEventDispatcher = _characterBody.gameObject.GetComponent<AnimationEventDispatcher>();

        _controlProperty = _playerController.controlProperty;
        //Debug.Log("Player State Machine : ������Ʈ �ʱ�ȭ");

        // ������Ʈ �ʱ�ȭ
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        //Debug.Log("Player State Machine : ������Ʈ �ʱ�ȭ");

        // �÷��̾� �Է� �ݹ� ����
        _playerInput.PlayerCharacterInput.Move.started      += OnMovementInput;
        _playerInput.PlayerCharacterInput.Move.canceled     += OnMovementInput;
        _playerInput.PlayerCharacterInput.Move.performed    += OnMovementInput;
        _playerInput.PlayerCharacterInput.Walk.started  += OnWalkInput;
        _playerInput.PlayerCharacterInput.Walk.canceled += OnWalkInput;
        _playerInput.PlayerCharacterInput.Run.performed   += OnRunInput;
        _playerInput.PlayerCharacterInput.Run.canceled += OnRunInput;
        _playerInput.PlayerCharacterInput.Attack.started += OnAttackInput;
        _playerInput.PlayerCharacterInput.Attack.canceled+= OnAttackInput;
        _playerInput.PlayerCharacterInput.Guard.started += OnGuardInput;
        _playerInput.PlayerCharacterInput.Guard.canceled+= OnGuardInput;
        _playerInput.PlayerCharacterInput.Dodge.started += OnDodgeInputPress;
        _playerInput.PlayerCharacterInput.Dodge.canceled += OnDodgeInputRelease;
        _playerInput.PlayerCharacterInput.SwitchArm.performed += OnSwitchingArmInput;
        //_playerInput.PlayerCharacterInput.SwitchArm.started += (InputAction.CallbackContext context) => Debug.Log("SwitchArm Started");
        //_playerInput.PlayerCharacterInput.SwitchArm.performed += (InputAction.CallbackContext context) => Debug.Log("SwitchArm performed");
        //_playerInput.PlayerCharacterInput.SwitchArm.canceled += (InputAction.CallbackContext context) => Debug.Log("SwitchArm canceled");
        Debug.Log("Player State Machine : ��ǲ ���ε�");

        // _playerInput.PlayerCharacterInput.

    }
    // Start is called before the first frame update
    void Start()
    {
        _cameraInput = _cameraController.cm_FreeLook.GetComponent<CinemachineInputProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
        // 임시?
        RotateCharacterBody();
    }

    public void SetMoveDirection()
    {
        SetCurrentMovement();

        if (!_isMovementPressed)
            return;

        Vector3 newDirection = Vector3.zero;
        // ĳ���� ȸ�� * �ӽ��ϼ��� ����
        if (_cameraController.CameraState == ECameraState.DEFAULT || _isRunPressed || _combatController.IsDodging)
            newDirection = _currentMovement;
        else if (_cameraController.CameraState == ECameraState.LOCKON)
            newDirection = _cameraController.cameraArm.forward;

        SetDirection(newDirection);

        _appliedMovement = _currentMovement;
    }

    public Vector3 CalculateCurrentMovement()
    {
        Vector3 newCurMovement = Vector3.zero;
        
        return newCurMovement;
    }
    public void SetCurrentMovement()
    {
        Vector3 inputDir = new Vector3(_currentMovementInput.x, 0f, _currentMovementInput.y);

        _controlProperty.axisValue = new Vector2(inputDir.x, inputDir.z);
        _controlProperty.speed = Mathf.Ceil(Mathf.Abs(inputDir.x) + Mathf.Abs(inputDir.z) / 2f);

        Transform cameraArm = _cameraController.cameraArm;
        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        _currentMovement = lookForward * inputDir.z + lookRight * inputDir.x;
    }

    public void SetDirection(Vector3 newBodyDirection)
    {
        // _currentDirection = newBodyDirection;
        _characterBody.forward = newBodyDirection;
        // transform.forward = newBodyDirection;
    }

    public void RotateCharacterBody()
    {
        //if (!_canRotate)
        //    return;

        // _characterBody.rotation = Quaternion.Lerp(_characterBody.rotation, Quaternion.Euler(_currentDirection), 100f);
    }

    // �Է� �ݹ� �Լ�
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        SetCurrentMovement();
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
    }
    void OnRunInput(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
        Debug.Log($"Run Input : {_isRunPressed}");
    }
    void OnWalkInput(InputAction.CallbackContext context)
    {
        _isWalkPressed = context.ReadValueAsButton();
    }
    void OnAttackInput(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    }
    void OnGuardInput(InputAction.CallbackContext context)
    {
        _isGuardPressed = context.ReadValueAsButton();
    }

    /**
     * 구르기 입력 로직
     * Press와 Release 함수로 나누어서 시간 체크하고 
     * 시간값과 무브값으로 구르기인지 백스텝인지 Release에서 검사
     * 
     */
    void OnDodgeInputPress(InputAction.CallbackContext context)
    {
        _dodgeStartTime = Time.time;
    }
    void OnDodgeInputRelease(InputAction.CallbackContext context)
    {
        float pressRate = Time.time - _dodgeStartTime;
        Debug.Log($"Dodge Press Rate : {pressRate}");
        if (!(pressRate < _dodgePressedRate))
            return;

        if (_currentMovementInput != Vector2.zero)
        {
            Debug.LogWarning($"isRollPressed, Read Value : {context.ReadValueAsButton()}");
            _isRollPressed = true;
        }
        else
        {
            Debug.LogWarning($"isBackStepPressed, Read Value : {context.ReadValueAsButton()}");
            _isBackStepPressed = true;
        }
    }

    void OnSwitchingArmInput(InputAction.CallbackContext context)
    {
        _isSwitchingArmPressed = context.ReadValueAsButton();
        if(_isSwitchingArmPressed ) 
        {
            _equipmentController.SwitchArmState();
        }

    }
    public void LockInput()
    {
        _playerInput.PlayerCharacterInput.Disable();
        _cameraInput.enabled = false;
    }
    public void ResetInput()
    {
        _playerInput.PlayerCharacterInput.Enable();
        _cameraInput.enabled = true;
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
