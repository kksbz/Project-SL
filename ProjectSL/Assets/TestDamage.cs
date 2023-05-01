using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    Transform playerTR;
    // Start is called before the first frame update
    void Start()
    {
        playerTR = GFunc.GetRootObj("PlayerCharacter").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        GData.IDamageable damageable = other.gameObject.GetComponent<GData.IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(gameObject, 10f);
            enabled = false;
            StartCoroutine(Reset());
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        enabled = true;
    }
}
