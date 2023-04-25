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
        VisibleTargets = GFunc.FindTargetInRange(_transform, _viewRadius, _viewAngle, _targetMask, _obstacleMask);
    }
}
