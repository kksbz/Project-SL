using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject quickBar; // 퀵바
    public GameObject bonfirePanel; // 화톳불 패널
    public GameObject interactionBar; // 상호작용 오브젝트
    public TMP_Text interactionText; // 상호작용 텍스트
    public WarpController warp; // 화톳불 워프 컨트롤러
    public LoadingPanel loadingPanel; // 로딩 패널
    public GameObject levelUpPanel; // 레벨업 패널
    public GameObject shopPanel; // 상점 패널
    public StatusPanel statusPanel; // 스테이터스 패널
    public StatusPanel invenStatusPanel; // 인벤토리 스테이터스 패널
    public OptionPanel optionPanel; // 옵션 패널
    public MessagePanel messagePanel; // 메시지 패널
    public QuickSlotBar quickSlotBar; // 퀵슬롯바
    public SoulBagUi soulBag; // 소울가방UI
    public HealthSystemHUD healthHud; // 체력,마력,스태미너 바

    //! 인벤토리 스테이터스 패널 갱신하는 함수
    public void RenewalInvenStatusPanel()
    {
        invenStatusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData());
    } // RenewalstatusPanel

    //! 스테이터스 패널 갱신하는 함수
    public void RenewalStatusPanel()
    {
        statusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData(), Inventory.Instance.weaponSlotList);
    } // RenewalStatusPanel
} // UiManager

