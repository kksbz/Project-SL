using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WarpSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private WarpController warpController;
    [SerializeField] private Button warpBt;
    [SerializeField] private TMP_Text warpText;
    [SerializeField] private GameObject warpSlotEffect;
    public BonfireData bonfire;
    // Start is called before the first frame update
    void Start()
    {
        warpController = UiManager.Instance.warp;
        warpText.text = bonfire.bonfireName;

        warpBt.onClick.AddListener(() =>
        {
            warpController.selectWarp = this;
            warpController.warpSelect.SetActive(true);
        });
    } // Start

    private void OnDisable()
    {
        warpSlotEffect.SetActive(false);
    } // OnDisable

    //! 마우스 커서 들어올 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        warpSlotEffect.SetActive(true);
    } // OnPointerEnter

    public void OnPointerExit(PointerEventData eventData)
    {
        warpSlotEffect.SetActive(false);
    } // OnPointerExit
} // WarpSlot
