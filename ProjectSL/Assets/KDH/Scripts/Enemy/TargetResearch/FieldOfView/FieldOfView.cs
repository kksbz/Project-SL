using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IFieldOfView : GData.IInitialize
{
    List<Transform> VisibleTargets { get; }
    IEnumerator FieldOfViewCoroutine(float delay);
}

[Serializable]
public class FieldOfView : IFieldOfView
{
    private Transform _transform;
    private EnemyResearchStatus _researchStatus;

    // 시야 영역의 반지름과 시야 각도
    private float _viewRadius;
    private float _viewAngle;

    // 마스크 2종
    private LayerMask _targetMask, _obstacleMask;

    // Target mask에 ray hit된 transform을 보관하는 리스트
    public List<Transform> VisibleTargets { get; private set; }

    public FieldOfView(Transform newTransform, EnemyResearchStatus newResearchStatus)
    {
        _transform = newTransform;
        _researchStatus = newResearchStatus;
    }

    public void Init()
    {
        VisibleTargets = new List<Transform>();
        _viewRadius = _researchStatus.viewRadius;
        _viewAngle = _researchStatus.viewAngle;
        _targetMask = _researchStatus.targetLayerMask;
        _obstacleMask = _researchStatus.obstacleLayerMask;
    }

    public IEnumerator FieldOfViewCoroutine(float delay)
    {
        VisibleTargets.Clear();
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        VisibleTargets.Clear();
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(_transform.position, _viewRadius, _targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - _transform.position).normalized;

            // forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(_transform.forward, dirToTarget) < _viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(_transform.position, target.transform.position);

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
                if (!Physics.Raycast(_transform.position, dirToTarget, dstToTarget, _obstacleMask))
                {
                    VisibleTargets.Add(target);
                }
            }
        }
    }
}
