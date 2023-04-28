using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterBase, IPlayerDataAccess
{
    public static PlayerCharacter Instance { get { return instance; } }

    public CharacterController characterController { get; private set; }
    public PlayerController playerController { get; private set; }
    public CameraController cameraController { get; private set; }
    public CombatController combatController { get; private set; }
    public AnimationController animationController { get; private set; }
    public Animator animator { get; private set; }
    public CharacterControlProperty controlProperty { get; private set; }

    // Status Field
    private HealthSystem _healthSystem = new HealthSystem();
    private PlayerStatus _status = new PlayerStatus();

    public BehaviorStateMachine SM_Behavior { get; private set; }
    public LookStateMachine SM_Look { get; private set; }

    private static PlayerCharacter instance;
    // {�׽�Ʈ ������ �޼� ���
    public GameObject leftArm;
    public GameObject rightArm;
    // }�׽�Ʈ ������ �޼� ���
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            characterController = GetComponent<CharacterController>();
            playerController = GetComponent<PlayerController>();
            cameraController = GetComponent<CameraController>();
            combatController = GetComponent<CombatController>();
            animationController = GetComponent<AnimationController>();
            GameObject ownMeshObj = gameObject.FindChildObj("Mesh");
            animator = ownMeshObj.GetComponent<Animator>();

            GameManager.Instance.playerRightArm = rightArm;
            GameManager.Instance.playerLeftArm = leftArm;
            GameManager.Instance.player = this;
            return;
        }
        DestroyImmediate(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        InitBehaviorStateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        // SM_Behavior?.UpdateState();
        // SM_Look?.UpdateState();
    }
    private void FixedUpdate()
    {
        // SM_Behavior?.FixedUpdateState();
        // SM_Look?.FixedUpdateState();
    }
    private void InitBehaviorStateMachine()
    {
        SM_Behavior = new BehaviorStateMachine(EBehaviorStateName.IDLE, new PC_BState_Idle(playerController));
        SM_Behavior.AddState(EBehaviorStateName.MOVE, new PC_BState_Move(playerController));
        SM_Behavior.AddState(EBehaviorStateName.ATTACK, new PC_BState_Attack(playerController, animator, combatController));
    }
    private void InitLookStateMachine()
    {
        // SM_Look = new LookStateMachine(ELookStateName.FREELOOK, new )
    }

    //! { �÷��̾� ������ ���̺� �� �ε��ϴ� �������̽��Լ�
    public PlayerStatus GetPlayerData()
    {
        return _status;
    }
    public void LoadPlayerData(PlayerStatus _playerStatusData)
    {
        _status = _playerStatusData;
    }

    public HealthSystem GetHealth()
    {
        return _healthSystem;
    }
    //! } �÷��̾� ������ ���̺� �� �ε��ϴ� �������̽��Լ�
}
