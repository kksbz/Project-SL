using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerController playerController;

    private CharacterControlProperty controlProperty;

    // 임시로 기본 애니메이터 컨트롤러 캐싱해두고 사용하기 * 임시 아닐수도 있음
    [SerializeField]
    private RuntimeAnimatorController _defaultAnimatorController;

    [SerializeField]
    private RuntimeAnimatorController _currentAnimatorController;

    public RuntimeAnimatorController DefaultAnimatorController { get { return _defaultAnimatorController; } }

    private float speed;
    private float axisX;
    private float axisY;
    private bool isLockOn;

    // constants layer index
    public static readonly int LAYERINDEX_BASELAYER         = 0;
    public static readonly int LAYERINDEX_GUARDLAYER        = 1;
    public static readonly int LAYERINDEX_RUNLAYER          = 2;
    public static readonly int LAYERINDEX_WALKLAYER         = 3;
    public static readonly int LAYERINDEX_UPPERLAYER        = 4;
    public static readonly int LAYERINDEX_ARMLAYER          = 5;
    public static readonly int LAYERINDEX_TRANSITIONLAYER   = 6;
    public static readonly int LAYERINDEX_FULLLAYER         = 7;
    public float Speed
    {
        set { speed = value; }
    }
    public Vector2 AxisValue
    {
        set
        {
            axisX = value.x;
            axisY = value.y;
        }
    }
    public bool IsLockOn
    {
        set { isLockOn = value; }
    }

    private void Awake()
    {
        GameObject meshObj = gameObject.FindChildObj("Mesh");
        animator = meshObj.GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        controlProperty = playerController.controlProperty;
        _defaultAnimatorController = animator.runtimeAnimatorController as AnimatorController;
        _currentAnimatorController = _defaultAnimatorController;
        // animator.
        //_torsoAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationProperties();
    }
    void UpdateAnimationProperties()
    {
        animator.SetFloat("Speed", controlProperty.speed);
        animator.SetFloat("Horizontal", controlProperty.axisValue.x);
        animator.SetFloat("Vertical", controlProperty.axisValue.y);
        animator.SetBool("IsLockOn", controlProperty.isLockOn);
    }
    public void SetWeight(int layerIndex, float weight)
    {
        animator.SetLayerWeight(layerIndex, weight);
    }
    public void InitializeAnimController()
    {
        animator.runtimeAnimatorController = _currentAnimatorController;
    }
    public void SetAnimatorControllerState(RuntimeAnimatorController newAnimController)
    {
        _currentAnimatorController = newAnimController;
        animator.runtimeAnimatorController = newAnimController;
    }
}
