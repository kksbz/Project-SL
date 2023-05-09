using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : CharacterBase, IPlayerDataAccess, GData.IDamageable
{
    public static PlayerCharacter Instance { get { return instance; } }

    public Rigidbody rigidBody { get; private set; }

    public CharacterController characterController { get; private set; }
    public PlayerController playerController { get; private set; }
    public CameraController cameraController { get; private set; }
    public CombatController combatController { get; private set; }
    public AnimationController animationController { get; private set; }
    public Animator animator { get; private set; }
    public CharacterControlProperty controlProperty { get; private set; }
    public EquipmentController equipmentController { get; private set; }

    // Status Field
    private HealthSystem _healthSystem;// = new HealthSystem(this);
    private PlayerStatus _status = new PlayerStatus();
    private CombatStatus _combatStatus = new CombatStatus();

    private PlayerStateMachine _stateMachine;

    public PlayerStateMachine StateMachine { get { return _stateMachine; } }
    public HealthSystem HealthSys { get { return _healthSystem; } }
    public PlayerStatus PlayerStat { get { return _status; } }
    public CombatStatus CombatStat { get { return _combatStatus; } }

    public BehaviorStateMachine SM_Behavior { get; private set; }
    public LookStateMachine SM_Look { get; private set; }

    private static PlayerCharacter instance;
    [SerializeField]
    private Transform _hitTR;
    // {�׽�Ʈ ������ �޼� ���
    public GameObject leftArm;
    public GameObject rightArm;
    // }�׽�Ʈ ������ �޼� ���
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _healthSystem = new HealthSystem(this);
            rigidBody = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
            playerController = GetComponent<PlayerController>();
            cameraController = GetComponent<CameraController>();
            combatController = GetComponent<CombatController>();
            animationController = GetComponent<AnimationController>();
            _stateMachine = GetComponent<PlayerStateMachine>();
            equipmentController = GetComponent<EquipmentController>();
            GameObject ownMeshObj = gameObject.FindChildObj("Mesh");
            animator = ownMeshObj.GetComponent<Animator>();
            // _hitTR = ownMeshObj.transform;

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
        //equipmentController._onChangedAbilityStat += (EquipmentController) => Debug.Log($"app vit : {_status.AppliedVitality}");
        //equipmentController._onChangedAbilityStat += _status.AdderEquipmentValueInit;
        //equipmentController._onChangedAbilityStat += (EquipmentController) => Debug.Log($"app vit : {_status.AppliedVitality}");
        //equipmentController._onChangedAbilityStat += UpdateHealthAndCombatStatusByStatus;
        
        equipmentController._onChangedAbilityStat += (equipmentController) =>
        {
            _status.AdderEquipmentValueInit(equipmentController);
            _healthSystem.InitializeHealthSystem(_status, equipmentController);
            _combatStatus.InitializeCombatStatus(_status);
        };
        
        // 테스트용 코루틴
        StartCoroutine(TestStatInit());
        // StatInitialize();
    }
    public void UpdateHealthAndCombatStatusByStatus(EquipmentController equipmentController)
    {
        // _status.AdderEquipmentValueInit(equipmentController);
        _healthSystem.InitializeHealthSystem(_status, equipmentController);
        _combatStatus.InitializeCombatStatus(_status);
        Debug.Log($"Applied Vitality : {_status.AppliedVitality}");
    }

    [SerializeField]
    Transform testCube;
    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"Applied Vital : {_status.AppliedVitality}");
        _healthSystem.RegenerationStamina();
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
    public StatusSaveData GetPlayerData()
    {
        StatusSaveData saveData = new StatusSaveData(_status, _healthSystem, gameObject.transform.position);
        return saveData;
    }

    public void LoadPlayerData(StatusSaveData _playerStatusData)
    {
        _status = _playerStatusData._playerStatusData;
        StatInitialize();
    }

    public void PlayerNameSelect(string _name)
    {
        _status.Name = _name;
    }

    #region Stat Initialize

    // 스탯(헬스, 컴뱃스탯) 초기화 타입 열거
    /*
    public enum EStatusInitializeType : byte
    {
        ChangedStatus,
        ChangedEquipment,
        SaveDataLoad_AfterDead,
        SaveDataLoad_FromTitleScene
    }
    */
    IEnumerator TestStatInit()
    {
        yield return new WaitForSeconds(1f);
        StatInitialize();
    }
    public void StatInitialize()
    {
        if (this._healthSystem == null)
            return;
        if (this._combatStatus == null)
            return;
        

        // 체력 초기화 * 임시 아이템 스탯 정보도 추가해야되나?
        _healthSystem.InitializeHealthSystem(_status, equipmentController);
        _combatStatus.InitializeCombatStatus(_status);

        // 현재 체력, 마나를 맥스로 회복시켜주는 부분인데, 세이브데이터 로드시
        // 세이브데이터 내 현제체력 무시하고 회복시키니까 수정 드갈것 같음
        _healthSystem.HealHP(_healthSystem.MaxHP);
        _healthSystem.HealMP(_healthSystem.MaxMP);
        //

    }
    #endregion  // Stat Initialize
    //! } �÷��̾� ������ ���̺� �� �ε��ϴ� �������̽��Լ�
    void GData.IDamageable.TakeDamage(GameObject damageCauser, float damage)
    {
        if(UiInPutManager.Instance.isInvincibility == true)
        {
            return;
        }
        
        string hitDir = GetHitAngle(_hitTR, damageCauser.transform);
        Debug.Log($"direction : {hitDir}");
        combatController.HitAnimationTag = hitDir;
        if (combatController.HitAnimationTag == "Hit_Light_F" && (_stateMachine.CurrentState is PlayerGuardState || _stateMachine.CurrentState is PlayerBlockState))
        {
            _stateMachine.BlockFlag = true;
        }
        else
        {
            _healthSystem.Damage(damage);
            _stateMachine.HitFlag = true;
        }
        //float angle2 = GetAngleBetween3DVector(damageCauser.transform.position - transform.position, transform.forward);
        //Debug.Log($"angle2 : {angle2}");
    }
    string GetHitAngle(Transform hit, Transform causer)
    {
        string resultStr = string.Empty;
        Vector3 direction = (hit.position - causer.position).normalized;
        direction.y = 0f;
        float angle = Vector3.Angle(hit.forward, direction);

        Vector3 cross = Vector3.Cross(hit.forward, direction);
        Debug.Log($"angle : {angle}");

        if (cross.y < 0f)
        {
            angle = 360 - angle;
        }
        //

        if (angle >= 45f && angle < 135f)
        {
            resultStr = "Hit_Light_L";
        }
        else if (angle >= 135f && angle < 225f)
        {
            resultStr = "Hit_Light_F";
        }
        else if (angle >= 225f && angle < 315f)
        {
            resultStr = "Hit_Light_R";
        }
        else
        {
            resultStr = "Hit_Light_B";
        }
        /*
        if (cross.y < 0f)
        {
            if(angle < 45f)
            {
                resultStr = "Hit_Light_F";
            }
            else if(angle < 135f)
            {
                resultStr = "Hit_Light_R";
            }
            else
            {
                resultStr = "Hit_Light_B";
            }
        }
        else
        {
            if(angle < 45f)
            {
                resultStr = "Hit_Light_B";
            }
            else if(angle < 135f)
            {
                resultStr = "Hit_Light_L";
            }
            else
            {
                resultStr = "Hit_Light_F";
            }
        }
        */
        return resultStr;
    }
    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public float GetAngleBetween3DVector(Vector3 vec1, Vector3 vec2)
    {
        float theta = Vector3.Dot(vec1, vec2) / (vec1.magnitude * vec2.magnitude);
        Vector3 dirAngle = Vector3.Cross(vec1, vec2);
        float angle = Mathf.Acos(theta) * Mathf.Rad2Deg;
        if (dirAngle.z < 0.0f) angle = 360 - angle;
        Debug.Log("사잇각 : " + angle);
        return angle;
    }
}
