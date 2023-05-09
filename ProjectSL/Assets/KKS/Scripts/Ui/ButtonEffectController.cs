using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject buttonEffect; // 버튼 이펙트
    //! 마우스 커서 들어올 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonEffect.SetActive(true);
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonEffect.SetActive(false);
    } // OnPointerExit

    private void OnEnable()
    {
        buttonEffect.SetActive(false);
    } // OnDisable
} // ButtonEffectController
