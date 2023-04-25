using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GData
{
    public interface IInitialize
    {
        void Init();
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }

    public interface IGiveDamageable
    {
        void GiveDamage(IDamageable damageable, float damage);
    }
}
