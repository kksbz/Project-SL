using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Normal Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage = 10f;
    public float staminaCost = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
