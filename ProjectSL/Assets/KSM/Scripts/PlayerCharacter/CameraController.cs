using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ECameraState
{
    NONE = -1,
    DEFAULT,
    LOCKON
}

public class CameraController : MonoBehaviour
{
    public Transform cameraArm;
    private CharacterBase target;

    private CharacterController characterController;
    private AnimationController animationController;

    private ECameraState cameraState;
    public ECameraState CameraState
    {
        get { return cameraState; }
    }

    // { LockOn
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private List<Transform> targetsInView;
    // } LockOn

    public delegate void EventHandler_void_GameObject(GameObject gObj_);
    public delegate void EventHandler_void();
    public EventHandler_void_GameObject onSetPlayerLockOn;
    public EventHandler_void onReleasePlayerLockOn;

    [Header("Temp Debugging �� �����")]
    [Range(1f, 100f)]
    public float rotationLerpSpeed = 10f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<AnimationController>();
        //onSetPlayerLockOn = new EventHandler_void_GameObject(SetPlayerLockOn);
        //onReleasePlayerLockOn = new EventHandler_void(ReleasePlayerLockOn);
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraState = ECameraState.DEFAULT;
        targetsInView = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();
        InputAction();
        LookAround();
        LookTarget();
    }

    void InputAction()
    {
        Input_LockOn();
    }
    void Input_LockOn()
    {
        if (Input.GetMouseButtonDown(2))
        {
            switch (cameraState)
            {
                case ECameraState.DEFAULT:
                    SetPlayerLockOn();
                    break;
                case ECameraState.LOCKON:
                default:
                    ReleasePlayerLockOn();
                    break;
            }
        }
    }
    void LookAround()
    {
        // ���� ������ ���
        if(IsLockOn)
        {
            return;
        }
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

        cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
    }
    void LookTarget()
    {
        // ���� ���°� �ƴ� ���
        if(!IsLockOn)
        {
            return;
        }
        // �ӽ�
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - cameraArm.position);
        cameraArm.rotation = Quaternion.Lerp(cameraArm.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        // cameraArm.transform.LookAt(target.transform,);
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
            Debug.Log($"target Angle : {angle}");
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
        GameObject newTarget = default;
        float minDistance = float.MaxValue;

        // ���� ����� ĳ���� ã��
        foreach(var targetTf in targetsInView)
        {
            Debug.Log("����� ĳ���� ã����");
            Vector3 offset = targetTf.position - transform.position;
            float sqrLen = offset.sqrMagnitude;
            if(minDistance > sqrLen)
            {
                minDistance = sqrLen;
                newTarget = targetTf.gameObject;
            }
        }

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
        animationController.IsLockOn = true;
        cameraState = ECameraState.LOCKON;
    }
    void ReleasePlayerLockOn()
    {
        // ���� ���°� �ƴ� ���
        if(!IsLockOn)
        {
            return;
        }
        target = default;
        animationController.IsLockOn = true;
        cameraState = ECameraState.DEFAULT;
    }
    public bool IsLockOn
    {
        get
        {
            return cameraState == ECameraState.LOCKON;
        }
    }

}
