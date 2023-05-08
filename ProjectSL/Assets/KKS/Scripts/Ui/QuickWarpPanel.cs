using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickWarpPanel : MonoBehaviour
{
    public GameObject quickWarpPanel; // �����̵� �г�
    public GameObject WarningPanel; // ��� �г�
    [SerializeField] private Button selectBt; // ���ù�ư
    [SerializeField] private Button cancleBt; // ��ҹ�ư

    private void Start()
    {
        // ���� ��ư
        selectBt.onClick.AddListener(() =>
        {
            int soul = Inventory.Instance.Soul;
            UiManager.Instance.soulBag.GetSoul(-soul);
            BonfireData pontiffBonfire = default;
            foreach (BonfireData _bonfire in UiManager.Instance.warp.bonfireList)
            {
                if (_bonfire.bonfireID == 0)
                {
                    pontiffBonfire = _bonfire;
                    break;
                }
            }
            gameObject.SetActive(false);
            UiManager.Instance.quickBar.SetActive(false);
            GameManager.Instance.LoadBonfire(pontiffBonfire);
        });
        // ��� ��ư
        cancleBt.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    } // Start

    private void OnDisable()
    {
        quickWarpPanel.SetActive(false);
        WarningPanel.SetActive(false);
    } // OnDisable

    private void Update()
    {
        if (WarningPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    } // Update
} // QuickWarpPanel
