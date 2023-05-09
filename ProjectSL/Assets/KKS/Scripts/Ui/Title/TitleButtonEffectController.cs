using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButtonEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject btEffect;

    private void OnDisable()
    {
        btEffect.SetActive(false);
    } // OnDisable

    //! 마우스 커서 들어올 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        btEffect.SetActive(true);
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        btEffect.SetActive(false);
    } // OnPointerExit
} // TitleButtonEffectController
