using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamageable : MonoBehaviour, GData.IDamageable
{
    void GData.IDamageable.TakeDamage(GameObject damageCauser, float damage)
    {
        Debug.Log($"{name} Damaged");
    }
}
