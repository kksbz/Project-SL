using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class ButtonEffectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject buttonEffect; // ��ư ����Ʈ
    //! ���콺 Ŀ�� ���� �� ����
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
