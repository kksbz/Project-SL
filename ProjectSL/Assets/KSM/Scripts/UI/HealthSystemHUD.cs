using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystemHUD : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _decHpBar;
    [SerializeField] private Image _mpBar;
    [SerializeField] private Image _decMpBar;
    [SerializeField] private Image _spBar;
    [SerializeField] private Image _decSpBar;

    // UI Property
    public Image HP_Bar { get { return _hpBar; } }
    public Image HP_Dec_Bar { get { return _decHpBar; } }
    public Image MP_Bar { get { return _mpBar; } }
    public Image MP_Dec_Bar { get { return _decMpBar; } }
    public Image SP_Bar { get { return _spBar; } }
    public Image SP_Dec_Bar { get { return _decSpBar; } }

    // UI Action
    public Action<Image, float> ChangeProgressImmediate;
    public Action<Image, float> ChangeProgressLerp;

    // Start is called before the first frame update
    void Start()
    {
        ChangeProgressImmediate += SetHealthProgressImmediate;
        ChangeProgressLerp += SetHealthProgressLerp;
    }
    #region UI Control
    
    void SetHealthProgressImmediate(Image targetUI, float ratio)
    {
        targetUI.fillAmount = ratio;
    }
    public void SetHealthProgressLerp(Image targetUI, float ratio)
    {
        StartCoroutine(LerpProgress(targetUI, targetUI.fillAmount, ratio));
    }
    IEnumerator LerpProgress(Image targetUI, float startRatio, float targetRatio)
    {
        yield return new WaitForSeconds(1f);
        float curveValue = 0f;
        float curveDelta = 0.02f;
        while (targetUI.fillAmount >= targetRatio)
        {
            targetUI.fillAmount = Mathf.Lerp(startRatio, targetRatio, curveValue);
            curveValue += curveDelta;
            yield return null;
        }
        Debug.Log("코루틴 루프 탈출");
    }
    #endregion  // UI Control
}
