using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject optionBar; // 옵션바
    public GameObject bonfirePanel; // 화톳불 패널
    public GameObject interactionBar; // 상호작용 오브젝트
    public TMP_Text interactionText; // 상호작용 텍스트
    public WarpController warp; // 화톳불 워프 컨트롤러
    public GameObject loadingPanel; // 로딩화면
    public QuickSlotBar quickSlotBar; // 퀵슬롯바
} // UiManager

