using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, GData.IGiveDamageable
{
    public Transform target;
    public float damage;
    public Rigidbody rockRigidbody;

    private void OnEnable()
    {

    }

    public void Throwing()
    {
        transform.LookAt(target);
        rockRigidbody.AddForce(transform.forward * 15.0f, ForceMode.Impulse);
        transform.Rotate(0, 90, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        GData.IDamageable damageable = other.GetComponent<GData.IDamageable>();

        if (damageable == null || damageable == default)
        {
            /*  Do Nothine  */
        }
        else
        {
            GiveDamage(damageable, damage);
        }

        //Destroy(gameObject);
    }

    public void GiveDamage(GData.IDamageable damageable, float damage)
    {
        damageable.TakeDamage(damage);
    }
}
