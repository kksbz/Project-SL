using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enrage : MonoBehaviour, GData.IGiveDamageable
{
    // 전멸기 패턴
    // 필요한 정보

    // 데미지
    public float damage;

    [Tooltip("딜레이")]
    [SerializeField]
    private float _delay;

    [Tooltip("폭발 반경")]
    [SerializeField]
    private float _radius;
    // 폭발 각도
    private const float _ANGLE = 360f;

    [Tooltip("타겟 LayerMask")]
    [SerializeField]
    private LayerMask _targetMask;

    [Tooltip("장애물 LayerMask")]
    [SerializeField]
    private LayerMask _obstacleMask;

    public GameObject pillar;

    public GameObject unActiveObj;
    public GameObject[] activeObj;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private void OnEnable()
    {
        StartCoroutine(EnrageDelay());
    }

    IEnumerator EnrageDelay()
    {
        yield return new WaitForSeconds(_delay);
        unActiveObj.SetActive(false);
        foreach (var iterator in activeObj)
        {
            iterator.SetActive(true);
        }
        Explosion();
        Debug.Log($"explosion End");

        yield return new WaitForSeconds(1f);

        Debug.Log($"explosion End / pillar : {pillar.name} / {pillar.GetComponent<Animator>().name}");

        pillar.GetComponent<Animator>().SetTrigger("DownPillar");
        unActiveObj.SetActive(true);
        foreach (var iterator in activeObj)
        {
            iterator.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        Destroy(pillar);

        gameObject.SetActive(false);

    }


    /// <summary>
    /// 
    /// </summary>
    public void Explosion()
    {
        audioSource.clip = audioClip;
        audioSource.Play();

        List<Transform> targets = GFunc.FindTargetInRange(transform, _radius, _ANGLE, _targetMask, _obstacleMask);
        foreach (var target in targets)
        {
            GData.IDamageable damageable = target.GetComponent<GData.IDamageable>();
            EnemyBase enemy_ = target.GetComponent<EnemyBase>();

            if (damageable == null || damageable == default || enemy_ != null || enemy_ != default)
            {
                /*  Do Nothing  */
            }
            else
            {
                GiveDamage(damageable, damage);
            }
        }
        Debug.Log($"explosion End Func");

    }

    public void GiveDamage(GData.IDamageable damageable, float damage)
    {
        Debug.Log($"데미지를 입힘 Enrage Debug / 대상 : {damageable.ToString()} / 데미지 : {damage}");
        damageable.TakeDamage(gameObject, damage);
    }

}
