using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyTargetResearch
{
    EnemyResearchStatus ResearchStatus { get; }
    IFieldOfView FieldOfView { get; }
    List<Transform> Targets { get; }
    void Init(EnemyResearchStatus newResearchStatus, IFieldOfView newFieldOfView);
    /// <summary>
    /// FOV 탐색 로직
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator FieldOfViewSearch(float delay);
    bool IsFieldOfViewFind { get; }
}

/// <summary>
/// 플레이어 혹은 적을 인식하기 위한 기능들을 구현할 클래스
/// </summary>
public class EnemyTargetResearch : MonoBehaviour, IEnemyTargetResearch
{
    public EnemyResearchStatus ResearchStatus { get; private set; }
    public IFieldOfView FieldOfView { get; private set; }

    [SerializeField]
    private List<Transform> _targets;
    public List<Transform> Targets { get { return _targets; } private set { _targets = value; } }

    public void Init(EnemyResearchStatus newResearchStatus, IFieldOfView newFieldOfView)
    {
        ResearchStatus = newResearchStatus;
        FieldOfView = newFieldOfView;
        FieldOfView.Init();
    }

    public IEnumerator FieldOfViewSearch(float delay)
    {
        return FieldOfView.FieldOfViewCoroutine(delay);
    }

    public bool IsFieldOfViewFind
    {
        get
        {
            if (0 < FieldOfView.VisibleTargets.Count)
            {
                Targets = FieldOfView.VisibleTargets;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
