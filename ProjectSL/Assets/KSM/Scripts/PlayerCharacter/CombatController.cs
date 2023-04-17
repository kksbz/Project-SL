using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private PlayerController playerController;
    private CharacterControlProperty controlProperty;

    // Attack �ӽ�
    public LayerMask EnemyMask;
    public float attackRadius;
    
    public List<AttackSO> combo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter = 0;

    public bool canAttack = true;
    private bool canNextCombo = default;
    private bool isExecuteImmediateNextCombo = default;
    private bool isAttacking = false;
    private bool isComboInputOn = default;
    private int currentCombo = 0;
    private int maxCombo = default;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controlProperty = playerController.controlProperty;
        maxCombo = combo.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        NextAttackCheck();
        ExitAttack();
    }
    void Attack()
    {
        // ���� ��������
        if (!canAttack)
            return;

        AttackLogic();
    }
    void AttackLogic()
    {
        // ���Ӱ��� üũ (�޺�)
        if (isAttacking)
        {
            if(currentCombo < 1 || currentCombo >= maxCombo)
                return;
            if(canNextCombo)
            {
                isComboInputOn = true;
            }
        }
        // ù ���� üũ
        else
        {
            if (currentCombo != 0)
                return;
            AttackStartComboState();
            AttackAnimationPlay();
            isAttacking = true;
            controlProperty.isAttacking = true;
        }
    }
    void AttackStartComboState()
    {
        canNextCombo = false;
        isComboInputOn = false;
        isExecuteImmediateNextCombo = false;

        if (!CheckComboAssert(currentCombo, 0, maxCombo))
        {
            return;
        }
        currentCombo = Mathf.Clamp(currentCombo + 1, 1, maxCombo);
    }
    void AttackEndComboState()
    {
        canNextCombo = false;
        isExecuteImmediateNextCombo = false;
        currentCombo = 0;
        isAttacking = false;
        controlProperty.isAttacking = false;
    }
    bool CheckComboAssert(int current, int start, int max)
    {
        bool isValid = true;
        if(current < start)
            isValid = false;
        if(current >= max)
            isValid = false;
        Debug.Log($"isValid = {isValid}");
        return isValid;
    }
    void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(4).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(4).IsTag("Attack"))
        {
            AttackEndComboState();
        }
    }
    void AttackAnimationPlay()
    {
        animator.runtimeAnimatorController = combo[currentCombo - 1].animatorOV;
        animator.Play("Attack", 4, 0);
    }
    // �ִϸ��̼� �̺�Ʈ
    public void Event_SetCanNextCombo()
    {
        canNextCombo = true;
    }
    public void Event_SetCantNextCombo()
    {
        canNextCombo = false;
    }
    public void Event_SetOnExecuteNextCombo()
    {
        isExecuteImmediateNextCombo= true;
    }
    public void Event_SetOffExecuteNextCombo()
    {
        isExecuteImmediateNextCombo = false;
    }
    public void NextAttackCheck()
    {
        if(!isComboInputOn)
        {
            return;
        }
        if(!isExecuteImmediateNextCombo)
        {
            return;
        }
        AttackStartComboState();
        AttackAnimationPlay();
    }
    public void AttackCheck()
    {
        Debug.LogWarning("Attack Check");
        Vector3 center = transform.position + transform.forward;
        Collider[] hitResults = Physics.OverlapSphere(center, attackRadius, EnemyMask);
        foreach(Collider hitResult in hitResults) 
        {
            Debug.LogWarning($"{hitResult.gameObject.name} �¾���");

        }
    }

}
