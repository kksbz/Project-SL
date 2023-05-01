using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] GameObject titleMenu;
    [SerializeField] GameObject selectEffect;
    private TMP_Text pressText;
    private bool isPresskey = false;
    private void Start()
    {
        pressText = gameObject.GetComponent<TMP_Text>();
        StartCoroutine(FadeInSelf());
    } // Start

    void Update()
    {
        if (isPresskey == true && Input.anyKeyDown)
        {
            StartCoroutine(FadeOutSelf());
        }
    } // Update

    //! 페이드인 하는 코루틴함수
    private IEnumerator FadeInSelf()
    {
        pressText.color = new Color(1, 1, 1, 0);
        float time = 1.5f;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0f, 1f, elapsedTime / time);
            pressText.color = new Color(1, 1, 1, alphaValue);
            yield return null;
        }
        selectEffect.SetActive(true);
        isPresskey = true;
    } // FadeInSelf

    //! 페이드아웃 하는 코루틴함수
    private IEnumerator FadeOutSelf()
    {
        selectEffect.SetActive(false);
        float time = 1.5f;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / time);
            pressText.color = new Color(1, 1, 1, alphaValue);
            yield return null;
        }
        titleMenu.SetActive(true);
        gameObject.SetActive(false);
    } // FadeOutSelf
} // PressAnyKey
