using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject optionBar; // �ɼǹ�
    public GameObject bonfirePanel; // ȭ��� �г�
    public GameObject interactionBar; // ��ȣ�ۿ� ������Ʈ
    public TMP_Text interactionText; // ��ȣ�ۿ� �ؽ�Ʈ
    public WarpController warp; // ȭ��� ���� ��Ʈ�ѷ�
    public GameObject loadingPanel; // �ε�ȭ��
    public QuickSlotBar quickSlotBar; // �����Թ�
    public SoulBagUi soulBag; // �ҿﰡ��UI
} // UiManager

