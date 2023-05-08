using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickWarpPanel : MonoBehaviour
{
    public GameObject quickWarpPanel; // 빠른이동 패널
    public GameObject WarningPanel; // 경고 패널
    [SerializeField] private Button selectBt; // 선택버튼
    [SerializeField] private Button cancleBt; // 취소버튼

    private void Start()
    {
        // 선택 버튼
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
            GameManager.Instance.LoadBonfire(pontiffBonfire);
            UiManager.Instance.quickBar.SetActive(false);
            gameObject.SetActive(false);
        });
        // 취소 버튼
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
