using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class TitleButtonEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject btEffect;

    private void OnDisable()
    {
        btEffect.SetActive(false);
    } // OnDisable

    //! ���콺 Ŀ�� ���� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        btEffect.SetActive(true);
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        btEffect.SetActive(false);
    } // OnPointerExit
} // TitleButtonEffectController
