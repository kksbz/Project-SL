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

    [SerializeField]
    float _lockOnLimitDist = 10f;

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

    [Header("Temp Debugging �� �����")]
    [Range(1f, 100f)]
    public float rotationLerpSpeed = 10f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<AnimationController>();

        _meshObj = gameObject.FindChildObj("Mesh").transform;
        GameObject cameraObj = GFunc.GetRootObj("Main Camera");
        camera = cameraObj.transform;
        cm_FreeLook = cameraObj.FindChildObj("CM FreeLook");
        cm_LockOn = cameraObj.FindChildObj("CM LockOnCam");

        //onSetPlayerLockOn = new EventHandler_void_GameObject(SetPlayerLockOn);
        //onReleasePlayerLockOn = new EventHandler_void(ReleasePlayerLockOn);
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraState = ECameraState.DEFAULT;
        targetsInView = new List<Transform>();
        controlProperty = playerController.controlProperty;

        // Ŀ�� �Ⱥ��̰�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        TargetDeadCheck();
        SearchTarget();
        TargetDeadCheck();
        TargetDistanceCheck();
        InputAction();
    }
    private void FixedUpdate()
    {
        LookAround();
        LookTarget();
        CameraArmFollowMesh();
    }
    private void TargetDeadCheck()
    {
        if(IsLockOn && target == null)
        {
            Debug.Log("TargetDeadCheck");
            GameObject newTargetObject = FindNearestTarget();
            if(newTargetObject != null)
            {
                CharacterBase newTargetCharacter = newTargetObject.GetComponent<CharacterBase>();
                if (newTargetCharacter != null)
                {
                    Debug.Log("TargetDeadCheck newTarget is not null");
                    target = newTargetCharacter;
                    UpdateLockTargets();
                }
                else
                {
                    Debug.Log("TargetDeadCheck newTarget is null");
                    ReleasePlayerLockOn();
                    UpdateLockTargets();
                }
            }
            else
            {
                ReleasePlayerLockOn();
                UpdateLockTargets();
            }
        }
    }
    private void TargetDistanceCheck()
    {
        if (!IsLockOn)
            return;

        if (target == null)
            return;

        float toTargetDistance = Vector3.Distance(transform.position, target.gameObject.transform.position);
        if (toTargetDistance > _lockOnLimitDist)
        {
            ReleasePlayerLockOn();
            UpdateLockTargets();
        }
    }
    void InputAction()
    {
        Input_LockOn();
    }
    void Input_LockOn()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("�� �Է� �޳���?");
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
            targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
            targetGroup.AddMember(transform, 0.9f, 0f);
            targetGroup.AddMember(target.gameObject.FindChildObj("LockOnTarget").transform, 0.6f, 0);

            camera.gameObject.GetComponent<CinemachineController>().LockCamera();
            camera.gameObject.GetComponent<CinemachineController>().FollowTarget(cameraArm);
            camera.gameObject.GetComponent<CinemachineController>().SetLookAt(targetGroup.transform);
        }
        else
        {
            camera.gameObject.GetComponent<CinemachineController>().FreeCamera();
            
        }
    }
    void LookAround()
    {
        // ���� ������ ���
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
        // ���� ���°� �ƴ� ���
        if(!IsLockOn)
        {
            return;
        }
        if (target == null)
            return;
        // �ӽ�
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
        // int layerMask = 1 << targetMask;
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        foreach(var targetCol in targets)
        {
            Transform targetTf = targetCol.transform;
            Vector3 direction = (targetTf.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, cameraArm.forward);
            // Debug.Log($"target Angle : {angle}");
            if (angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance, targetMask))
                {
                    Debug.Log($"hit : {hit.transform.gameObject.name}");
                    tempTargetsInView.Add(hit.transform);
                }
            }
        }

        bool result = Enumerable.SequenceEqual(tempTargetsInView, targetsInView);
        if(!result)
        {
            Debug.Log("targetsInView ����");
            targetsInView = tempTargetsInView;
        }
    }

    void SetPlayerLockOn()
    {
        Debug.Log("Enter SetPlayerLockOn");
        // �̹� ���� ������ ���
        if (IsLockOn)
        {
            return;
        }
        Debug.Log("���»��� �ƴ�");
        GameObject newTarget = FindNearestTarget();

        // �þ߹����� ĳ���Ͱ� ���� ���
        if (newTarget == default)
        {
            Debug.Log("�þ߹����� ĳ���� ����");
            return;
        }

        target = newTarget.GetComponent<CharacterBase>();

        // Ÿ���� ĳ���ͺ��̽��� �������ִ��� üũ
        if (target == null || target == default)
        {
            return;
        }
        Debug.Log("���»��·� ����");
        controlProperty.isLockOn = true;
        cameraState = ECameraState.LOCKON;
    }
    GameObject FindNearestTarget()
    {
        GameObject searchTarget = default;
        float minDistance = float.MaxValue;
        foreach (var targetTf in targetsInView)
        {
            if(targetTf == null)
                continue;

            Debug.Log("����� ĳ���� ã����");
            Vector3 offset = targetTf.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
            if (minDistance > sqrLen)
            {
                minDistance = sqrLen;
                searchTarget = targetTf.gameObject;
            }
        }
        return searchTarget;
    }
    void ReleasePlayerLockOn()
    {
        // ���� ���°� �ƴ� ���
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
