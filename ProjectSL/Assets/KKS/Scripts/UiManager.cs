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
    public LoadingPanel loadingPanel; // �ε�ȭ��
    public StatusPanel statusPanel; // �������ͽ� ȭ��
    public StatusPanel invenStatusPanel; // �κ��丮 �������ͽ� ȭ��
    public OptionPanel optionPanel; // �ɼ� ȭ��
    public QuickSlotBar quickSlotBar; // �����Թ�
    public SoulBagUi soulBag; // �ҿﰡ��UI

    //! �κ��丮 �������ͽ� �г� �����ϴ� �Լ�
    public void RenewalInvenStatusPanel()
    {
        invenStatusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData(),
                GameManager.Instance.player.GetHealth());
    } // RenewalstatusPanel

    //! �������ͽ� �г� �����ϴ� �Լ�
    public void RenewalStatusPanel()
    {
        statusPanel.ShowPlayerStatusPanel(GameManager.Instance.player.GetPlayerData(),
            GameManager.Instance.player.GetHealth(), quickSlotBar.RightWeaponList, quickSlotBar.LeftWeaponList);
    } // RenewalStatusPanel
} // UiManager

