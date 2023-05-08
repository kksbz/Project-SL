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
    public Animator animator;
    [SerializeField]
    private PlayerCharacter playerCharacter;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private AnimationController animationController;
    [SerializeField]
    private EquipmentController equipmentController;
    [SerializeField]
    private PlayerStateMachine playerStateMachine;

    [SerializeField]
    private QuickSlotBar _quickSlotBar;

    private float tempMoveSpeed = 5f;

    public CharacterControlProperty controlProperty = new CharacterControlProperty();

    [SerializeField]
    private CharacterController characterController = default;

    #region ItemAction Field
    [SerializeField]
    ItemActionSO _currentItemAction_Attack;
    [SerializeField]
    ItemActionSO _currentItemAction_Recovery;
    int _currentItemActionAnimationLayer;
    string _itemActionAnimationTag;
    bool _isUsingItem;
    bool _canContinuous;
    bool _isContinuousInputOn;
    #endregion  // ItemAction Field
    // �ӽ�
    public Vector3 moveDir;
    public Vector3 inputDir;
    public Move nextMove;
    public Behavior wait;
    bool isMove;

    #region Action Costs
    public float _rollActionCost = 15;
    public float _backStepActionCost = 12;
    public float _BlockActionCost = 30;
    public float _sprintActionCost = 15;
    #endregion  // Action Costs

    #region Die Field
    bool _isDie;
    #endregion  // Die Field

    public bool IsDie { get { return _isDie; } }
    public ItemActionSO ItemAction_Recovery { get { return _currentItemAction_Recovery; } set { _currentItemAction_Recovery = value; } }
    public ItemActionSO ItemAction_Attack { get { return _currentItemAction_Attack; } set { _currentItemAction_Attack = value; } }
    private void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController = GetComponent<CharacterController>();
        cameraController = GetComponent<CameraController>();
        animationController = GetComponent<AnimationController>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        equipmentController = GetComponent<EquipmentController>();

        _quickSlotBar = UiManager.Instance.quickSlotBar;

        GameObject meshObj = gameObject.FindChildObj("Mesh");
        characterBody = meshObj.transform;
        animator = meshObj.GetComponent<Animator>();

    }   // Awake()
    // Start is called before the first frame update
    void Start()
    {
        AnimationEventDispatcher _animEventDispatcher = animator.gameObject.GetComponent<AnimationEventDispatcher>();

        _animEventDispatcher.onAnimationEnd.AddListener(EndedItemActionAnimation);
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

    #region Die
    public void Die()
    {
        if (_isDie)
            return;
        _isDie = true;

        DieAnimationPlay();
    }

    void DieAnimationPlay()
    {
        PoseAction poseAction = new PoseAction(animator, "Die_Light_1", AnimationController.LAYERINDEX_FULLLAYER, 0);
        // nextPA = poseAction;
        poseAction.Execute();
        StartCoroutine(EndDieAnimation());
    }

    private IEnumerator EndDieAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        UiManager.Instance.messagePanel.PlayerDeadMessage();
    }
    #endregion  // Die

    #region UseItem
    public void UseRecoveryItem()
    {
        if (!CanUseRecoveryItem())
            return;
        
        UseRecoveryItemStartState();
        UseRecoveryItemLogic();
        UseItemAnimationPlay();
        playerStateMachine.UseItemFlag = true;
        _isUsingItem = true;
    }
    // 애니메이션 설정
    void UseRecoveryItemLogic()
    {
        switch(_currentItemAction_Recovery._animationType)
        {
            case EItemActionAnimationType.Drink:
                _itemActionAnimationTag = "ItemAction_Drink";
                break;
            case EItemActionAnimationType.Throwing:
                _itemActionAnimationTag = "Throw";
                break;
        }
        if(_currentItemAction_Recovery._isWalkable)
        {
            _currentItemActionAnimationLayer = AnimationController.LAYERINDEX_UPPERLAYER;
            animator.SetLayerWeight(AnimationController.LAYERINDEX_UPPERLAYER, 1);
            animator.SetLayerWeight(AnimationController.LAYERINDEX_WALKLAYER, 1);
        }
        else
        {
            _currentItemActionAnimationLayer = AnimationController.LAYERINDEX_FULLLAYER;
        }
    }
    // 아이템 사용이 가능한지를 체크
    bool CanUseRecoveryItem()
    {
        bool canUseItem = true;

        /*if(!(playerStateMachine.CurrentState is PlayerGroundedState))
        {
            canUseItem = false;
        }*/
        if(!equipmentController.IsUsableRecoveryConsumption())
        {
            canUseItem = false;
        }

        //if()

        return canUseItem;
    }
    public void ConsumRecoveryItem()
    {
        // 갯수 줄이고
        if (_quickSlotBar.QuickSlotRecoveryConsumption == null)
            return;

        _quickSlotBar.QuickSlotRecoveryConsumption.Quantity = _quickSlotBar.QuickSlotRecoveryConsumption.Quantity - 1;
        playerCharacter.HealthSys.HealHP(_quickSlotBar.QuickSlotRecoveryConsumption.vigor);
        Inventory.Instance.InitSlotItemData();
        _quickSlotBar.LoadQuickSlotData();
    }

    void UseRecoveryItemStartState()
    {
        _canContinuous = false;
        _isContinuousInputOn = false;
        // 장비 숨기기
        equipmentController.HideLeftWeapon();
        equipmentController.ShowRecoveryConsumption();
    }
    void UseRecoveryItemEndState()
    {
        _canContinuous = false;
        _isContinuousInputOn = false;
        _isUsingItem = false;
        playerStateMachine.UseItemFlag = false;
        equipmentController.ShowLeftWeapon();
        equipmentController.HideRecoveryConsumption();
    }
    void ContinuousUseRecoveryItemCheck()
    {

    }
    void UseItemAnimationPlay()
    {
        Debug.Log("Play ItemAction Animation");
        PoseAction poseAction = new PoseAction(animator, _itemActionAnimationTag, _currentItemActionAnimationLayer, 0);
        poseAction.Execute();
    }
    #endregion  // UseItem

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
        // input�� ������ moveDirection ���
        if (isMove)
        {

            // ĳ���� ȸ�� * �ӽ��ϼ��� ����
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
    public void InitializeItemActionProperty()
    {
        UseRecoveryItemEndState();
    }
    public void InitializeAllProperty()
    {
        UseRecoveryItemEndState();
    }

    private bool IsItemActionAnimation(string name)
    {
        return name.StartsWith("ItemAction");
    }
    public void StartedItemActionAnimation(string name)
    {
        if (!IsItemActionAnimation(name))
            return;


    }
    public void EndedItemActionAnimation(string name)
    {
        if (!IsItemActionAnimation(name))
            return;

        Debug.Log("End ItemAction Animation");
        InitializeItemActionProperty();
        // 장비 숨겼다면 해제하기
        if(equipmentController.IsHideRightWeapon())
            equipmentController.ShowRightWeapon();
        if(equipmentController.IsHideLeftWeapon())
            equipmentController.ShowLeftWeapon();
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
        // �ӽ�
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
