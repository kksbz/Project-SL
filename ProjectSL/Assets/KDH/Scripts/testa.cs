using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testa : MonoBehaviour, GData.IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log($"데미지를 입음 damage : {damage}");
    }

}
