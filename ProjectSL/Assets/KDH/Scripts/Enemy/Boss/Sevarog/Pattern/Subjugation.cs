using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subjugation : MonoBehaviour
{
    [SerializeField]
    private Collider _collider = default;
    public float delay = default;
    public float damage = default;
    public GameObject explosion;

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        _collider.enabled = true;
        explosion.SetActive(true);
        Debug.Log($"폭발");
        yield return new WaitForSeconds(0.3f);
        _collider.enabled = false;
        Debug.Log($"삭제");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GData.IDamageable object_ = other.GetComponent<GData.IDamageable>();

        if (object_ == null || object_ == default)
        {
            /*  Do Nothing  */
        }
        else
        {
            object_.TakeDamage(gameObject, damage);
        }
    }
}
