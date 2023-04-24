using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public enum ECameraState
{
    NONE = -1,
    DEFAULT,
    LOCKON
}

public class CameraController : MonoBehaviour
{
    public GameObject cm_FreeLook;
    public GameObject cm_LockOn;

    public Transform cameraArm;
    public Transform cameraTarget;
    public Transform camera;
    

    [SerializeField]
    private Animator cmCamAnimator;

    private PlayerController playerController;
    private CharacterController characterController;
    private AnimationController animationController;

    private CharacterControlProperty controlProperty;

    private Transform _meshObj;

    private ECameraState cameraState;
    public ECameraState CameraState
    {
        get { return cameraState; }
    }

    // { LockOn
    [Header("Lock On")]
    [SerializeField]
    private CharacterBase target;
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private List<Transform> targetsInView;
    // } LockOn

    [SerializeField]
    private Transform targetLocator;
    [SerializeField]
    private float crossHairScale;
    [SerializeField]
    private Canvas lockOnCanvas;
    [SerializeField]
    private Cinemachine.CinemachineTargetGroup targetGroup;

    public delegate void EventHandler_void_GameObject(GameObject gObj_);
    public delegate void EventHandler_void();
    public EventHandler_void_GameObject onSetPlayerLockOn;
    public EventHandler_void onReleasePlayerLockOn;

    [Header("Temp Debugging 다 지울거")]
    [Range(1f, 100f)]
    public float rotationLerpSpeed = 10f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<AnimationController>();

        _meshObj = gameObject.FindChildObj("Mesh").transform;
        //onSetPlayerLockOn = new EventHandler_void_GameObject(SetPlayerLockOn);
        //onReleasePlayerLockOn = new EventHandler_void(ReleasePlayerLockOn);
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraState = ECameraState.DEFAULT;
        targetsInView = new List<Transform>();
        controlProperty = playerController.controlProperty;

        // 커서 안보이게
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();
        InputAction();
    }
    private void FixedUpdate()
    {
        LookAround();
        LookTarget();
        CameraArmFollowMesh();
    }

    void InputAction()
    {
        Input_LockOn();
    }
    void Input_LockOn()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("휠 입력 받나요?");
            switch (cameraState)
            {
                case ECameraState.DEFAULT:
                    SetPlayerLockOn();
                    UpdateLockTargets();
                    break;
                case ECameraState.LOCKON:
                default:
                    ReleasePlayerLockOn();
                    UpdateLockTargets();
                    break;
            }
        }
    }

    private void UpdateLockTargets()
    {
        if (IsLockOn)
        {
            targetGroup.AddMember(transform, 0.9f, 0f);
            targetGroup.AddMember(target.gameObject.transform, 0.6f, 0);

            camera.gameObject.GetComponent<CinemachineController>().LockCamera();
            camera.gameObject.GetComponent<CinemachineController>().FollowTarget(cameraArm);
            camera.gameObject.GetComponent<CinemachineController>().SetLookAt(targetGroup.transform);
        }
        else
        {
            camera.gameObject.GetComponent<CinemachineController>().FreeCamera();
            targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
        }
    }
    void LookAround()
    {
        // 락온 상태일 경우
        if(IsLockOn)
        {
            return;
        }
        // Legacy Code
        /*
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 cameraAngle = cameraArm.rotation.eulerAngles;

        float x = cameraAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        */
        // cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
        cameraArm.rotation = ConvertRotationOnlyY(camera.rotation);
    }
    void LookTarget()
    {
        // 락온 상태가 아닐 경우
        if(!IsLockOn)
        {
            return;
        }
        // 임시
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - cameraArm.position);
        // camera.rotation = Quaternion.Lerp(camera.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        // cameraArm.rotation = Quaternion.Lerp(cameraArm.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        // cameraArm.transform.LookAt(target.transform,);
        cameraArm.rotation = ConvertRotationOnlyY(targetRotation);
        Debug.DrawLine(cameraArm.position, target.transform.position, Color.green);
    }
    private Vector3 BoundaryAngle(float angle)
    {
        angle += cameraArm.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    void SearchTarget()
    {
        List<Transform> tempTargetsInView = new List<Transform>();
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position, leftBoundary, Color.red);
        Debug.DrawRay(transform.position, rightBoundary, Color.red);

        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        foreach(var targetCol in targets)
        {
            
            Transform targetTf = targetCol.transform;
            Vector3 direction = (targetTf.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, cameraArm.forward);
            // Debug.Log($"target Angle : {angle}");
            if (angle < viewAngle * 0.5f)
            {
                Debug.Log($"angle Checking");
                RaycastHit hit;
                if(Physics.Raycast(transform.position, direction, out hit, viewDistance))
                {
                    Debug.Log($"hit : {hit.transform.gameObject.name}");
                    tempTargetsInView.Add(hit.transform);
                }
            }
        }

        bool result = Enumerable.SequenceEqual(tempTargetsInView, targetsInView);
        if(!result)
        {
            Debug.Log("targetsInView 갱신");
            targetsInView = tempTargetsInView;
        }
    }

    void SetPlayerLockOn()
    {
        Debug.Log("Enter SetPlayerLockOn");
        // 이미 락온 상태일 경우
        if (IsLockOn)
        {
            return;
        }
        Debug.Log("락온상태 아님");
        GameObject newTarget = default;
        float minDistance = float.MaxValue;

        // 가장 가까운 캐릭터 찾기
        foreach(var targetTf in targetsInView)
        {
            Debug.Log("가까운 캐릭터 찾는중");
            Vector3 offset = targetTf.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
            if(minDistance > sqrLen)
            {
                minDistance = sqrLen;
                newTarget = targetTf.gameObject;
            }
        }

        // 시야범위에 캐릭터가 없는 경우
        if (newTarget == default)
        {
            Debug.Log("시야범위에 캐릭터 없음");
            return;
        }

        target = newTarget.GetComponent<CharacterBase>();

        // 타겟이 캐릭터베이스를 가지고있는지 체크
        if (target == null || target == default)
        {
            return;
        }
        Debug.Log("락온상태로 변경");
        controlProperty.isLockOn = true;
        cameraState = ECameraState.LOCKON;
    }
    void ReleasePlayerLockOn()
    {
        // 락온 상태가 아닐 경우
        if(!IsLockOn)
        {
            return;
        }
        target = default;
        controlProperty.isLockOn = false;
        cameraState = ECameraState.DEFAULT;
    }
    public bool IsLockOn
    {
        get
        {
            return cameraState == ECameraState.LOCKON;
        }
    }

    Quaternion ConvertRotationOnlyY(Quaternion targetRotation)
    {
        Quaternion newTargetRotation = targetRotation;
        newTargetRotation.x = 0f;
        newTargetRotation.z = 0f;
        return newTargetRotation;
    }
    void CameraArmFollowMesh()
    {
        Vector3 targetPos = new Vector3(_meshObj.position.x, cameraArm.position.y, _meshObj.position.z);
        cameraArm.position = targetPos;// Vector3.Lerp(cameraArm.position, targetPos, Time.deltaTime * 5f);
    }
}
