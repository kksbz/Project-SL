using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerController playerController;

    private float speed;
    private float axisX;
    private float axisY;
    private bool isLockOn;
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
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationProperties();
    }
    void UpdateAnimationProperties()
    {
        Debug.Log($"speed {speed}");
        animator.SetFloat("Speed", speed);
        animator.SetFloat("Horizontal", axisX);
        animator.SetFloat("Vertical", axisY);
        animator.SetBool("IsLockOn", isLockOn);
    }
}
