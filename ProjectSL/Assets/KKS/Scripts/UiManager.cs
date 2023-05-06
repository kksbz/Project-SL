using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject quickBar; // ����
    public GameObject bonfirePanel; // ȭ��� �г�
    public GameObject interactionBar; // ��ȣ�ۿ� ������Ʈ
    public TMP_Text interactionText; // ��ȣ�ۿ� �ؽ�Ʈ
    public WarpController warp; // ȭ��� ���� ��Ʈ�ѷ�
    public LoadingPanel loadingPanel; // �ε� �г�
    public GameObject levelUpPanel; // ������ �г�
    public GameObject shopPanel; // ���� �г�
    public StatusPanel statusPanel; // �������ͽ� �г�
    public StatusPanel invenStatusPanel; // �κ��丮 �������ͽ� �г�
    public OptionPanel optionPanel; // �ɼ� �г�
    public QuickSlotBar quickSlotBar; // �����Թ�
    public SoulBagUi soulBag; // �ҿﰡ��UI
    public HealthSystemHUD healthHud; // ü��,����,���¹̳� ��

    //! �κ��丮 �������ͽ� �г� �����ϴ� �Լ�
    public void RenewalInvenStatusPanel()
    {
        invenStatusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData());
    } // RenewalstatusPanel

    //! �������ͽ� �г� �����ϴ� �Լ�
    public void RenewalStatusPanel()
    {
        statusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData(), Inventory.Instance.weaponSlotList);
    } // RenewalStatusPanel
} // UiManager

