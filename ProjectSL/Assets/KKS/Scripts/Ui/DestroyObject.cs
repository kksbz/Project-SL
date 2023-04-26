using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Animator objAni;
    private void Start()
    {
        objAni = GetComponent<Animator>();
        StartCoroutine(DestroyObj());
    }

    private IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(objAni.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
} // DestroyObject
